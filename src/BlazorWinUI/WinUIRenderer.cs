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

    public override Dispatcher Dispatcher => _dispatcher;

    internal Dictionary<int, NativeControl> NativeControls { get; } = [];
    internal AdapterResolver AdapterResolver { get; } = serviceProvider.GetService<AdapterResolver>()
        ?? AdapterResolver.CreateDefault(serviceProvider);

    public event EventHandler<UnhandledExceptionEventArgs>? OnUnhandledException;

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
        ArgumentNullException.ThrowIfNull(host);

        return Dispatcher.InvokeAsync(async () =>
        {
            var component = InstantiateComponent(typeof(TComponent));
            var componentId = AssignRootComponentId(component);
            var rootHost = new RootPanelHost(host);
            var root = new NativeControl(componentId, adapter: null, container: rootHost);

            NativeControls.Add(componentId, root);
            rootHost.SetChildren([]);
            await RenderRootComponentAsync(componentId, parameters);

            return componentId;
        });
    }

    protected override void HandleException(Exception exception)
    {
        _logger.LogError(exception, "Unhandled Exception in the WinUI Renderer");
        OnUnhandledException?.Invoke(this, new UnhandledExceptionEventArgs(exception, false));
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
            return;
        }

        var frames = GetCurrentRenderTreeFrames(componentId);
        var seen = new HashSet<int>();
        var desiredChildren = new List<NativeControl>();

        foreach (var frameIndex in EnumerateComponentFrames(frames.Array, 0, frames.Count))
        {
            var frame = frames.Array[frameIndex];
            var child = EnsureControl(frame, frames.Array, frameIndex);
            seen.Add(child.ComponentId);

            child.Parent ??= parent;

            desiredChildren.Add(child);
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
        control.Adapter?.Dispose();
    }

    private sealed class RootPanelHost(Panel panel) : IControlContainer
    {
        public void SetChildren(IReadOnlyList<FrameworkElement> children)
        {
            panel.Children.Clear();

            foreach (var child in children)
            {
                panel.Children.Add(child);
            }
        }
    }
}
