using Microsoft.AspNetCore.Components;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BlazorWinUI;

/// <summary>
/// Hosts one Blazor component in a WinUI XAML visual tree.
/// </summary>
public sealed class BlazorComponentHost : ContentControl
{
    public static readonly DependencyProperty RendererProperty = DependencyProperty.Register(
        nameof(Renderer),
        typeof(WinUIRenderer),
        typeof(BlazorComponentHost),
        new PropertyMetadata(null, OnRendererChanged));

    public static readonly DependencyProperty ComponentTypeProperty = DependencyProperty.Register(
        nameof(ComponentType),
        typeof(Type),
        typeof(BlazorComponentHost),
        new PropertyMetadata(null, OnComponentTypeChanged));

    public static readonly DependencyProperty ParametersProperty = DependencyProperty.Register(
        nameof(Parameters),
        typeof(IReadOnlyDictionary<string, object?>),
        typeof(BlazorComponentHost),
        new PropertyMetadata(null, OnParametersChanged));

    private readonly Grid _rootPanel = new();
    private WinUIRenderer? _mountedRenderer;
    private int? _rootComponentId;
    private Type? _mountedComponentType;
    private bool _hasBeenLoaded;
    private bool _isRestoringRenderer;
    private bool _isHostLoaded;
    private bool _unmountRequested;
    private bool _lifecycleUpdateRequested;
    private bool _isProcessingLifecycle;

    public BlazorComponentHost()
    {
        Content = _rootPanel;
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    /// <summary>
    /// The renderer for this host. Set it before the host is loaded and keep it unchanged for the
    /// lifetime of the host.
    /// </summary>
    public WinUIRenderer? Renderer
    {
        get => (WinUIRenderer?)GetValue(RendererProperty);
        set => SetValue(RendererProperty, value);
    }

    /// <summary>The Razor component type to render in this host.</summary>
    public Type? ComponentType
    {
        get => (Type?)GetValue(ComponentTypeProperty);
        set => SetValue(ComponentTypeProperty, value);
    }

    /// <summary>
    /// The root component parameters. Replacing this dictionary updates the mounted root; changes
    /// made inside the same dictionary instance are not observed automatically.
    /// </summary>
    public IReadOnlyDictionary<string, object?>? Parameters
    {
        get => (IReadOnlyDictionary<string, object?>?)GetValue(ParametersProperty);
        set => SetValue(ParametersProperty, value);
    }

    /// <summary>Raised for host configuration and lifecycle failures.</summary>
    public event EventHandler<System.UnhandledExceptionEventArgs>? HostError;

    private static void OnRendererChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        var host = (BlazorComponentHost)dependencyObject;
        if (host._isRestoringRenderer)
        {
            return;
        }

        if (host._hasBeenLoaded && !ReferenceEquals(args.OldValue, args.NewValue))
        {
            host._isRestoringRenderer = true;
            try
            {
                host.SetValue(RendererProperty, args.OldValue);
            }
            finally
            {
                host._isRestoringRenderer = false;
            }

            throw new InvalidOperationException(
                $"{nameof(Renderer)} must be set before {nameof(BlazorComponentHost)} is loaded and cannot be changed afterward.");
        }

        host.QueueLifecycleUpdate();
    }

    private static void OnComponentTypeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        if (args.NewValue is Type componentType && !IsValidComponentType(componentType))
        {
            throw new ArgumentException(
                $"ComponentType '{componentType.FullName}' must be a concrete, closed type that implements {nameof(IComponent)}.",
                nameof(ComponentType));
        }

        ((BlazorComponentHost)dependencyObject).QueueLifecycleUpdate();
    }

    private static void OnParametersChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        ((BlazorComponentHost)dependencyObject).QueueLifecycleUpdate();
    }

    private void OnLoaded(object sender, RoutedEventArgs args)
    {
        _hasBeenLoaded = true;
        _isHostLoaded = true;
        QueueLifecycleUpdate();
    }

    private void OnUnloaded(object sender, RoutedEventArgs args)
    {
        _isHostLoaded = false;
        _unmountRequested = true;
        QueueLifecycleUpdate();
    }

    private void QueueLifecycleUpdate()
    {
        if (!_isHostLoaded && !_rootComponentId.HasValue && _mountedRenderer is null)
        {
            return;
        }

        _lifecycleUpdateRequested = true;
        if (_isProcessingLifecycle)
        {
            return;
        }

        _isProcessingLifecycle = true;
        _ = ProcessLifecycleUpdatesAsync();
    }

    private async Task ProcessLifecycleUpdatesAsync()
    {
        try
        {
            while (_lifecycleUpdateRequested)
            {
                _lifecycleUpdateRequested = false;
                try
                {
                    await SynchronizeRootAsync();
                }
                catch (Exception exception)
                {
                    RaiseHostError(exception);
                }
            }
        }
        finally
        {
            _isProcessingLifecycle = false;
            if (_lifecycleUpdateRequested)
            {
                QueueLifecycleUpdate();
            }
        }
    }

    private async Task SynchronizeRootAsync()
    {
        if (_unmountRequested)
        {
            _unmountRequested = false;
            await UnmountCurrentRootAsync();
        }

        if (!_isHostLoaded || ComponentType is null)
        {
            await UnmountCurrentRootAsync();
            return;
        }

        var renderer = Renderer
            ?? throw new InvalidOperationException(
                $"Set the {nameof(Renderer)} property before loading a {nameof(BlazorComponentHost)} with a component type.");

        if (!renderer.Dispatcher.CheckAccess())
        {
            throw new InvalidOperationException(
                $"The {nameof(Renderer)} must use the DispatcherQueue associated with this host's UI thread.");
        }

        if (_rootComponentId is { } currentId && _mountedRenderer is not null)
        {
            if (_mountedComponentType == ComponentType)
            {
                await _mountedRenderer.UpdateRootComponentAsync(currentId, CreateParameterView(Parameters));
                return;
            }

            await UnmountCurrentRootAsync();
        }

        _rootComponentId = await renderer.MountRootComponentAsync(
            ComponentType,
            _rootPanel,
            CreateParameterView(Parameters));
        _mountedRenderer = renderer;
        _mountedComponentType = ComponentType;
    }

    private async Task UnmountCurrentRootAsync()
    {
        var renderer = _mountedRenderer;
        var componentId = _rootComponentId;
        if (renderer is null || componentId is null)
        {
            _mountedRenderer = null;
            _rootComponentId = null;
            _mountedComponentType = null;
            return;
        }

        try
        {
            await renderer.UnmountRootComponentAsync(componentId.Value);
        }
        finally
        {
            _mountedRenderer = null;
            _rootComponentId = null;
            _mountedComponentType = null;
        }
    }

    private void RaiseHostError(Exception exception)
    {
        HostError?.Invoke(this, new System.UnhandledExceptionEventArgs(exception, false));
    }

    private static ParameterView CreateParameterView(IReadOnlyDictionary<string, object?>? parameters)
    {
        if (parameters is null || parameters.Count == 0)
        {
            return ParameterView.Empty;
        }

        return ParameterView.FromDictionary(
            parameters.ToDictionary(pair => pair.Key, pair => pair.Value, StringComparer.Ordinal));
    }

    private static bool IsValidComponentType(Type componentType)
    {
        return componentType.IsClass &&
            !componentType.IsAbstract &&
            !componentType.ContainsGenericParameters &&
            typeof(IComponent).IsAssignableFrom(componentType);
    }
}
