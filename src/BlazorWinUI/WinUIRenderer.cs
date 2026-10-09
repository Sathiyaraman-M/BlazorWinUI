using System.Diagnostics.CodeAnalysis;

using BlazorWinUI.Abstractions;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;

using FrameworkElement = Microsoft.UI.Xaml.FrameworkElement;

namespace BlazorWinUI;

/// <summary>
/// Blazor renderer that projects native component adapters into a WinUI visual tree.
/// </summary>
public sealed class WinUIRenderer(IServiceProvider serviceProvider, DispatcherQueue dispatcherQueue, ILoggerFactory loggerFactory) : Renderer(serviceProvider, loggerFactory)
{
    private readonly WinUIDispatcher _dispatcher = new(dispatcherQueue);
    private readonly ILogger<WinUIRenderer> _logger = loggerFactory.CreateLogger<WinUIRenderer>();
    private readonly Dictionary<int, int> _componentParents = [];
    private readonly Dictionary<int, RootComponentRegistration> _rootsById = [];
    private readonly Dictionary<Panel, RootComponentRegistration> _rootsByPanel = new(ReferenceEqualityComparer.Instance);

    public override Dispatcher Dispatcher => _dispatcher;

    internal Dictionary<int, NativeControl> NativeControls { get; } = [];
    internal AdapterResolver AdapterResolver { get; } = serviceProvider.GetService<AdapterResolver>()
        ?? AdapterResolver.CreateDefault(serviceProvider);

    public event EventHandler<UnhandledExceptionEventArgs>? UnhandledException;

    /// <summary>
    /// Compatibility alias for <see cref="UnhandledException"/>.
    /// </summary>
    [Obsolete("Subscribe to UnhandledException instead.")]
    public event EventHandler<UnhandledExceptionEventArgs>? OnUnhandledException
    {
        add => UnhandledException += value;
        remove => UnhandledException -= value;
    }

    /// <summary>
    /// Registers the native adapter used to render a Blazor component.
    /// </summary>
    public void RegisterAdapter<TComponent, TAdapter>() where TComponent : IComponent where TAdapter : class, IAdapter
    {
        AdapterResolver.Register<TComponent, TAdapter>();
    }

    /// <summary>
    /// Mounts a root Blazor component into a WinUI panel.
    /// </summary>
    public Task<int> MountRootComponentAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>(Panel host) where TComponent : IComponent
    {
        return MountRootComponentAsync<TComponent>(host, ParameterView.Empty);
    }

    /// <summary>
    /// Mounts a root Blazor component into a WinUI panel with parameters.
    /// </summary>
    public Task<int> MountRootComponentAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>(Panel host, ParameterView parameters) where TComponent : IComponent
    {
        return MountRootComponentAsync(typeof(TComponent), host, parameters);
    }

    /// <summary>
    /// Mounts a new root Blazor component selected by runtime type with parameters.
    /// </summary>
    public Task<int> MountRootComponentAsync(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type componentType,
        Panel host,
        ParameterView parameters)
    {
        ArgumentNullException.ThrowIfNull(componentType);
        ArgumentNullException.ThrowIfNull(host);
        if (!IsValidComponentType(componentType))
        {
            throw new ArgumentException(
                $"Component type '{componentType.FullName}' must be a concrete, closed type that implements {nameof(IComponent)}.",
                nameof(componentType));
        }

        return Dispatcher.InvokeAsync(async () =>
        {
            if (_rootsByPanel.ContainsKey(host))
            {
                throw new InvalidOperationException(
                    "The WinUI panel already has a mounted root component. Update or unmount that root before mounting another.");
            }

            var component = InstantiateComponent(componentType);
            var componentId = AssignRootComponentId(component);
            var rootHost = new RootPanelHost(host);
            var root = new NativeControl(componentId, adapter: null, container: rootHost);
            var registration = new RootComponentRegistration(componentId, rootHost);

            NativeControls.Add(componentId, root);
            _rootsById.Add(componentId, registration);
            _rootsByPanel.Add(host, registration);
            rootHost.SetChildren([]);

            try
            {
                await RenderRootComponentAsync(componentId, parameters);
                return componentId;
            }
            catch
            {
                await UnmountRootComponentCoreAsync(componentId);
                throw;
            }
        });
    }

    /// <summary>
    /// Mounts a new root Blazor component selected by runtime type.
    /// </summary>
    public Task<int> MountRootComponentAsync(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type componentType,
        Panel host)
    {
        return MountRootComponentAsync(componentType, host, ParameterView.Empty);
    }

    /// <summary>
    /// Removes a mounted root component and disposes its component and native-control tree.
    /// </summary>
    public Task UnmountRootComponentAsync(int componentId)
    {
        return Dispatcher.InvokeAsync(() => UnmountRootComponentCoreAsync(componentId));
    }

    /// <summary>
    /// Updates the parameters of a mounted root component.
    /// </summary>
    public Task UpdateRootComponentAsync(int componentId, ParameterView parameters)
    {
        return Dispatcher.InvokeAsync(async () =>
        {
            if (!_rootsById.ContainsKey(componentId))
            {
                throw new InvalidOperationException(
                    $"Component ID {componentId} is not a mounted root component owned by this renderer.");
            }

            await RenderRootComponentAsync(componentId, parameters);
        });
    }

    protected override void HandleException(Exception exception)
    {
        _logger.LogError(exception, "Unhandled Exception in the WinUI Renderer");
        UnhandledException?.Invoke(this, new UnhandledExceptionEventArgs(exception, false));
    }

    protected override Task UpdateDisplayAsync(in RenderBatch renderBatch)
    {
        _dispatcher.AssertAccess();

        for (var i = 0; i < renderBatch.UpdatedComponents.Count; i++)
        {
            ApplyComponentRenderTree(renderBatch.UpdatedComponents.Array[i].ComponentId);
        }

        for (var i = 0; i < renderBatch.DisposedComponentIDs.Count; i++)
        {
            DisposeControls(renderBatch.DisposedComponentIDs.Array[i]);
        }

        return Task.CompletedTask;
    }

    private void ApplyComponentRenderTree(int componentId)
    {
        if (!NativeControls.TryGetValue(componentId, out var parent) ||
            parent.Container is not IControlContainer container)
        {
            if (TryFindNativeContainerAncestor(componentId, out var ancestor))
            {
                ApplyComponentRenderTree(ancestor.ComponentId);
            }

            return;
        }

        var seen = new HashSet<int>();
        var visitedComponents = new HashSet<int> { componentId };
        var desiredChildren = EnumerateVisualChildren(componentId, parent, seen, visitedComponents).ToList();

        if (parent.Adapter?.Element is Grid)
        {
            foreach (var child in desiredChildren)
            {
                var placement = child.GridCellPlacement;
                Grid.SetRow(child.Adapter!.Element, placement?.Row ?? 0);
                Grid.SetColumn(child.Adapter.Element, placement?.Column ?? 0);
                Grid.SetRowSpan(child.Adapter.Element, placement?.RowSpan ?? 1);
                Grid.SetColumnSpan(child.Adapter.Element, placement?.ColumnSpan ?? 1);
            }
        }

        var childrenChanged = parent.Children.Count != desiredChildren.Count;
        if (!childrenChanged)
        {
            for (var i = 0; i < desiredChildren.Count; i++)
            {
                if (parent.Children[i].ComponentId != desiredChildren[i].ComponentId)
                {
                    childrenChanged = true;
                    break;
                }
            }
        }

        for (var i = parent.Children.Count - 1; i >= 0; i--)
        {
            var child = parent.Children[i];
            if (!seen.Contains(child.ComponentId))
            {
                parent.Children.RemoveAt(i);
                DisposeSubtree(child);
            }
        }

        parent.Children.Clear();
        parent.Children.AddRange(desiredChildren);

        if (childrenChanged)
        {
            container.SetChildren([.. desiredChildren.Select(child => child.Adapter!.Element)]);
        }
    }

    private IEnumerable<NativeControl> EnumerateVisualChildren(
        int ownerComponentId,
        NativeControl nativeParent,
        HashSet<int> seenNativeControls,
        HashSet<int> visitedComponents,
        GridCellPlacement? gridCellPlacement = null)
    {
        var frames = GetCurrentRenderTreeFrames(ownerComponentId);

        foreach (var frameIndex in EnumerateComponentFrames(frames.Array, 0, frames.Count))
        {
            var frame = frames.Array[frameIndex];
            var childComponentId = frame.ComponentId;
            _componentParents[childComponentId] = ownerComponentId;

            if (frame.ComponentType == typeof(Components.GridCell))
            {
                if (nativeParent.Adapter?.Element is not Grid)
                {
                    throw new InvalidOperationException(
                        $"{nameof(Components.GridCell)} must be a descendant of a {nameof(Components.Grid)} component.");
                }

                var placement = ReadGridCellPlacement(frames.Array, frameIndex);
                if (visitedComponents.Add(childComponentId))
                {
                    foreach (var descendant in EnumerateVisualChildren(
                        childComponentId,
                        nativeParent,
                        seenNativeControls,
                        visitedComponents,
                        placement))
                    {
                        yield return descendant;
                    }
                }

                continue;
            }

            if (AdapterResolver.HasAdapter(frame.ComponentType))
            {
                var child = EnsureControl(frame, frames.Array, frameIndex);
                if (!seenNativeControls.Add(child.ComponentId))
                {
                    continue;
                }

                if (child.Parent is { } previousParent && !ReferenceEquals(previousParent, nativeParent))
                {
                    previousParent.Children.Remove(child);
                }

                child.Parent = nativeParent;
                child.GridCellPlacement = gridCellPlacement;
                yield return child;
                continue;
            }

            // Ordinary Razor components have no native adapter of their own. Flatten their
            // output into the nearest native container while keeping their component IDs in
            // the ownership chain so their later renders can trigger the same reconciliation.
            if (visitedComponents.Add(childComponentId))
            {
                foreach (var descendant in EnumerateVisualChildren(
                    childComponentId,
                    nativeParent,
                    seenNativeControls,
                    visitedComponents,
                    gridCellPlacement))
                {
                    yield return descendant;
                }
            }
        }
    }

    private static GridCellPlacement ReadGridCellPlacement(RenderTreeFrame[] frames, int frameIndex)
    {
        var row = 0;
        var column = 0;
        var rowSpan = 1;
        var columnSpan = 1;
        var end = Math.Min(frameIndex + frames[frameIndex].ComponentSubtreeLength, frames.Length);

        for (var index = frameIndex + 1; index < end; index++)
        {
            var frame = frames[index];
            if (frame.FrameType != RenderTreeFrameType.Attribute)
            {
                continue;
            }

            switch (frame.AttributeName)
            {
                case nameof(Components.GridCell.Row): row = (int)frame.AttributeValue!; break;
                case nameof(Components.GridCell.Column): column = (int)frame.AttributeValue!; break;
                case nameof(Components.GridCell.RowSpan): rowSpan = (int)frame.AttributeValue!; break;
                case nameof(Components.GridCell.ColumnSpan): columnSpan = (int)frame.AttributeValue!; break;
            }
        }

        return new GridCellPlacement(row, column, rowSpan, columnSpan);
    }

    private bool TryFindNativeContainerAncestor(int componentId, out NativeControl ancestor)
    {
        var visited = new HashSet<int>();
        var currentComponentId = componentId;

        while (_componentParents.TryGetValue(currentComponentId, out var parentComponentId) &&
               visited.Add(currentComponentId))
        {
            if (NativeControls.TryGetValue(parentComponentId, out var parent) &&
                parent.Container is not null)
            {
                ancestor = parent;
                return true;
            }

            currentComponentId = parentComponentId;
        }

        ancestor = null!;
        return false;
    }

    private NativeControl EnsureControl(RenderTreeFrame frame, RenderTreeFrame[] frames, int frameIndex)
    {
        if (NativeControls.TryGetValue(frame.ComponentId, out var existing))
        {
            ApplyParameters(existing.Adapter!, frames, frameIndex + 1, frame.ComponentSubtreeLength - 1);
            return existing;
        }

        var adapter = AdapterResolver.Create(frame.ComponentType);
        var control = new NativeControl(frame.ComponentId, adapter, adapter as IControlContainer);
        NativeControls.Add(control.ComponentId, control);
        ApplyParameters(adapter, frames, frameIndex + 1, frame.ComponentSubtreeLength - 1);
        return control;
    }

    private static IEnumerable<int> EnumerateComponentFrames(RenderTreeFrame[] frames, int start, int count)
    {
        var end = Math.Min(start + count, frames.Length);
        for (var index = start; index < end; index++)
        {
            var frame = frames[index];
            switch (frame.FrameType)
            {
                case RenderTreeFrameType.Component:
                    yield return index;
                    index += frame.ComponentSubtreeLength - 1;
                    break;
                case RenderTreeFrameType.Region:
                    foreach (var nested in EnumerateComponentFrames(
                        frames,
                        index + 1,
                        frame.RegionSubtreeLength - 1))
                    {
                        yield return nested;
                    }

                    index += frame.RegionSubtreeLength - 1;
                    break;
                case RenderTreeFrameType.Element:
                    throw new NotSupportedException(
                        $"HTML element '{frame.ElementName}' is not supported by the WinUI renderer.");
            }
        }
    }

    private static void ApplyParameters(IAdapter adapter, RenderTreeFrame[] frames, int start, int count)
    {
        var values = new Dictionary<string, object?>(StringComparer.Ordinal);
        var end = Math.Min(start + count, frames.Length);
        for (var index = start; index < end; index++)
        {
            var frame = frames[index];
            if (frame.FrameType == RenderTreeFrameType.Attribute)
            {
                values[frame.AttributeName] = frame.AttributeValue;
            }
        }

        adapter.ApplyParameters(ParameterView.FromDictionary(values));
    }

    private void DisposeControls(int componentId)
    {
        RemoveDescendantParentMappings(componentId);

        if (_rootsById.TryGetValue(componentId, out var rootRegistration))
        {
            RemoveRootRegistration(rootRegistration);
            rootRegistration.Host.SetChildren([]);
        }

        if (NativeControls.TryGetValue(componentId, out var control))
        {
            control.Parent?.Children.Remove(control);
            DisposeSubtree(control);
        }
    }

    private void DisposeSubtree(NativeControl control)
    {
        foreach (var child in control.Children.ToArray())
        {
            DisposeSubtree(child);
        }

        control.Children.Clear();
        NativeControls.Remove(control.ComponentId);
        _componentParents.Remove(control.ComponentId);
        control.Adapter?.Dispose();
    }

    private Task UnmountRootComponentCoreAsync(int componentId)
    {
        _dispatcher.AssertAccess();

        if (!_rootsById.TryGetValue(componentId, out var registration))
        {
            return Task.CompletedTask;
        }

        try
        {
            RemoveRootComponent(componentId);
        }
        finally
        {
            registration.Host.SetChildren([]);
            RemoveRootRegistration(registration);
            DisposeControls(componentId);
        }

        return Task.CompletedTask;
    }

    private static bool IsValidComponentType(Type componentType)
    {
        return componentType.IsClass &&
            !componentType.IsAbstract &&
            !componentType.ContainsGenericParameters &&
            typeof(IComponent).IsAssignableFrom(componentType);
    }

    private void RemoveRootRegistration(RootComponentRegistration registration)
    {
        _rootsById.Remove(registration.ComponentId);
        if (_rootsByPanel.TryGetValue(registration.Host.Panel, out var current) &&
            current.ComponentId == registration.ComponentId)
        {
            _rootsByPanel.Remove(registration.Host.Panel);
        }
    }

    private void RemoveDescendantParentMappings(int rootComponentId)
    {
        foreach (var componentId in _componentParents.Keys.ToArray())
        {
            var current = componentId;
            var visited = new HashSet<int>();
            while (visited.Add(current) && _componentParents.TryGetValue(current, out var parentId))
            {
                if (parentId == rootComponentId)
                {
                    _componentParents.Remove(componentId);
                    break;
                }

                current = parentId;
            }
        }

        _componentParents.Remove(rootComponentId);
    }

    private sealed class RootComponentRegistration(int componentId, RootPanelHost host)
    {
        public int ComponentId { get; } = componentId;

        public RootPanelHost Host { get; } = host;
    }

    private sealed class RootPanelHost(Panel panel) : IControlContainer
    {
        public Panel Panel { get; } = panel;

        public void SetChildren(IReadOnlyList<FrameworkElement> children)
        {
            Panel.Children.Clear();

            foreach (var child in children)
            {
                Panel.Children.Add(child);
            }
        }
    }
}
