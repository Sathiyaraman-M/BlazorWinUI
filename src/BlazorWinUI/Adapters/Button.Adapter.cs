#nullable enable
namespace BlazorWinUI.Adapters;

public sealed class ButtonAdapter : global::BlazorWinUI.Abstractions.IAdapter, global::BlazorWinUI.Abstractions.IControlContainer
{
    private readonly global::Microsoft.UI.Xaml.Controls.Button _control = new global::Microsoft.UI.Xaml.Controls.Button();
    private global::Microsoft.AspNetCore.Components.EventCallback _onClick;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Controls.FocusDisengagedEventArgs> _onFocusDisengaged;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Controls.FocusEngagedEventArgs> _onFocusEngaged;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DependencyPropertyChangedEventArgs> _onIsEnabledChanged;
    private global::Microsoft.AspNetCore.Components.EventCallback<object> _onActualThemeChanged;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DataContextChangedEventArgs> _onDataContextChanged;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.EffectiveViewportChangedEventArgs> _onEffectiveViewportChanged;
    private global::Microsoft.AspNetCore.Components.EventCallback<object> _onLayoutUpdated;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs> _onLoaded;
    private global::Microsoft.AspNetCore.Components.EventCallback<object> _onLoading;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.SizeChangedEventArgs> _onSizeChanged;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs> _onUnloaded;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.AccessKeyDisplayDismissedEventArgs> _onAccessKeyDisplayDismissed;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.AccessKeyDisplayRequestedEventArgs> _onAccessKeyDisplayRequested;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.AccessKeyInvokedEventArgs> _onAccessKeyInvoked;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.BringIntoViewRequestedEventArgs> _onBringIntoViewRequested;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.CharacterReceivedRoutedEventArgs> _onCharacterReceived;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs> _onContextCanceled;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ContextRequestedEventArgs> _onContextRequested;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs> _onDoubleTapped;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DragEventArgs> _onDragEnter;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DragEventArgs> _onDragLeave;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DragEventArgs> _onDragOver;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DragStartingEventArgs> _onDragStarting;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DragEventArgs> _onDrop;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DropCompletedEventArgs> _onDropCompleted;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.GettingFocusEventArgs> _onGettingFocus;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs> _onGotFocus;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.HoldingRoutedEventArgs> _onHolding;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs> _onKeyDown;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs> _onKeyUp;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.LosingFocusEventArgs> _onLosingFocus;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs> _onLostFocus;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ManipulationCompletedRoutedEventArgs> _onManipulationCompleted;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ManipulationDeltaRoutedEventArgs> _onManipulationDelta;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ManipulationInertiaStartingRoutedEventArgs> _onManipulationInertiaStarting;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ManipulationStartedRoutedEventArgs> _onManipulationStarted;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ManipulationStartingRoutedEventArgs> _onManipulationStarting;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.NoFocusCandidateFoundEventArgs> _onNoFocusCandidateFound;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> _onPointerCanceled;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> _onPointerCaptureLost;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> _onPointerEntered;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> _onPointerExited;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> _onPointerMoved;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> _onPointerPressed;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> _onPointerReleased;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> _onPointerWheelChanged;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs> _onPreviewKeyDown;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs> _onPreviewKeyUp;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ProcessKeyboardAcceleratorEventArgs> _onProcessKeyboardAccelerators;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.RightTappedRoutedEventArgs> _onRightTapped;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.TappedRoutedEventArgs> _onTapped;
    private readonly global::System.Collections.Generic.HashSet<string> _presentParameters = new(global::System.StringComparer.Ordinal);
    private readonly object _defaultText;
    private readonly string? _defaultAutomationName;
    private readonly object? _defaultToolTip;
    private readonly bool _defaultIsEnabled;
    private readonly global::Microsoft.UI.Xaml.HorizontalAlignment _defaultHorizontalAlignment;
    private readonly global::Microsoft.UI.Xaml.VerticalAlignment _defaultVerticalAlignment;
    private readonly global::Microsoft.UI.Xaml.Controls.Primitives.FlyoutBase _defaultFlyout;
    private readonly global::Microsoft.UI.Xaml.Controls.ClickMode _defaultClickMode;
    private readonly global::System.Windows.Input.ICommand _defaultCommand;
    private readonly object _defaultCommandParameter;
    private readonly global::Microsoft.UI.Xaml.DataTemplate _defaultContentTemplate;
    private readonly global::Microsoft.UI.Xaml.Controls.DataTemplateSelector _defaultContentTemplateSelector;
    private readonly global::Microsoft.UI.Xaml.Media.Animation.TransitionCollection _defaultContentTransitions;
    private readonly global::Microsoft.UI.Xaml.Media.Brush _defaultBackground;
    private readonly global::Microsoft.UI.Xaml.Controls.BackgroundSizing _defaultBackgroundSizing;
    private readonly global::Microsoft.UI.Xaml.Media.Brush _defaultBorderBrush;
    private readonly global::Microsoft.UI.Xaml.Thickness _defaultBorderThickness;
    private readonly int _defaultCharacterSpacing;
    private readonly global::Microsoft.UI.Xaml.CornerRadius _defaultCornerRadius;
    private readonly global::System.Uri _defaultDefaultStyleResourceUri;
    private readonly global::Microsoft.UI.Xaml.ElementSoundMode _defaultElementSoundMode;
    private readonly global::Microsoft.UI.Xaml.Media.FontFamily _defaultFontFamily;
    private readonly double _defaultFontSize;
    private readonly global::Windows.UI.Text.FontStretch _defaultFontStretch;
    private readonly global::Windows.UI.Text.FontStyle _defaultFontStyle;
    private readonly global::Windows.UI.Text.FontWeight _defaultFontWeight;
    private readonly global::Microsoft.UI.Xaml.Media.Brush _defaultForeground;
    private readonly global::Microsoft.UI.Xaml.HorizontalAlignment _defaultHorizontalContentAlignment;
    private readonly bool _defaultIsFocusEngaged;
    private readonly bool _defaultIsFocusEngagementEnabled;
    private readonly bool _defaultIsTextScaleFactorEnabled;
    private readonly global::Microsoft.UI.Xaml.Thickness _defaultPadding;
    private readonly global::Microsoft.UI.Xaml.Controls.RequiresPointer _defaultRequiresPointer;
    private readonly global::Microsoft.UI.Xaml.Input.KeyboardNavigationMode _defaultTabNavigation;
    private readonly global::Microsoft.UI.Xaml.Controls.ControlTemplate _defaultTemplate;
    private readonly global::Microsoft.UI.Xaml.VerticalAlignment _defaultVerticalContentAlignment;
    private readonly bool _defaultAllowFocusOnInteraction;
    private readonly bool _defaultAllowFocusWhenDisabled;
    private readonly object _defaultDataContext;
    private readonly global::Microsoft.UI.Xaml.FlowDirection _defaultFlowDirection;
    private readonly global::Microsoft.UI.Xaml.Thickness _defaultFocusVisualMargin;
    private readonly global::Microsoft.UI.Xaml.Media.Brush _defaultFocusVisualPrimaryBrush;
    private readonly global::Microsoft.UI.Xaml.Thickness _defaultFocusVisualPrimaryThickness;
    private readonly global::Microsoft.UI.Xaml.Media.Brush _defaultFocusVisualSecondaryBrush;
    private readonly global::Microsoft.UI.Xaml.Thickness _defaultFocusVisualSecondaryThickness;
    private readonly double _defaultHeight;
    private readonly string _defaultLanguage;
    private readonly global::Microsoft.UI.Xaml.Thickness _defaultMargin;
    private readonly double _defaultMaxHeight;
    private readonly double _defaultMaxWidth;
    private readonly double _defaultMinHeight;
    private readonly double _defaultMinWidth;
    private readonly string _defaultName;
    private readonly global::Microsoft.UI.Xaml.ElementTheme _defaultRequestedTheme;
    private readonly global::Microsoft.UI.Xaml.ResourceDictionary _defaultResources;
    private readonly global::Microsoft.UI.Xaml.Style _defaultStyle;
    private readonly object _defaultTag;
    private readonly double _defaultWidth;
    private readonly string _defaultAccessKey;
    private readonly global::Microsoft.UI.Xaml.DependencyObject _defaultAccessKeyScopeOwner;
    private readonly bool _defaultAllowDrop;
    private readonly global::Microsoft.UI.Xaml.Media.CacheMode _defaultCacheMode;
    private readonly bool _defaultCanBeScrollAnchor;
    private readonly bool _defaultCanDrag;
    private readonly global::System.Numerics.Vector3 _defaultCenterPoint;
    private readonly global::Microsoft.UI.Xaml.Media.RectangleGeometry _defaultClip;
    private readonly global::Microsoft.UI.Xaml.Media.ElementCompositeMode _defaultCompositeMode;
    private readonly global::Microsoft.UI.Xaml.Controls.Primitives.FlyoutBase _defaultContextFlyout;
    private readonly bool _defaultExitDisplayModeOnAccessKeyInvoked;
    private readonly global::Microsoft.UI.Xaml.ElementHighContrastAdjustment _defaultHighContrastAdjustment;
    private readonly bool _defaultIsAccessKeyScope;
    private readonly bool _defaultIsDoubleTapEnabled;
    private readonly bool _defaultIsHitTestVisible;
    private readonly bool _defaultIsHoldingEnabled;
    private readonly bool _defaultIsRightTapEnabled;
    private readonly bool _defaultIsTabStop;
    private readonly bool _defaultIsTapEnabled;
    private readonly double _defaultKeyTipHorizontalOffset;
    private readonly global::Microsoft.UI.Xaml.Input.KeyTipPlacementMode _defaultKeyTipPlacementMode;
    private readonly global::Microsoft.UI.Xaml.DependencyObject _defaultKeyTipTarget;
    private readonly double _defaultKeyTipVerticalOffset;
    private readonly global::Microsoft.UI.Xaml.Input.KeyboardAcceleratorPlacementMode _defaultKeyboardAcceleratorPlacementMode;
    private readonly global::Microsoft.UI.Xaml.DependencyObject _defaultKeyboardAcceleratorPlacementTarget;
    private readonly global::Microsoft.UI.Xaml.Input.ManipulationModes _defaultManipulationMode;
    private readonly double _defaultOpacity;
    private readonly global::Microsoft.UI.Xaml.ScalarTransition _defaultOpacityTransition;
    private readonly global::Microsoft.UI.Xaml.Media.Projection _defaultProjection;
    private readonly double _defaultRasterizationScale;
    private readonly global::Microsoft.UI.Xaml.Media.Transform _defaultRenderTransform;
    private readonly global::Windows.Foundation.Point _defaultRenderTransformOrigin;
    private readonly float _defaultRotation;
    private readonly global::System.Numerics.Vector3 _defaultRotationAxis;
    private readonly global::Microsoft.UI.Xaml.ScalarTransition _defaultRotationTransition;
    private readonly global::System.Numerics.Vector3 _defaultScale;
    private readonly global::Microsoft.UI.Xaml.Vector3Transition _defaultScaleTransition;
    private readonly global::Microsoft.UI.Xaml.Media.Shadow _defaultShadow;
    private readonly global::Microsoft.UI.Xaml.Input.KeyboardNavigationMode _defaultTabFocusNavigation;
    private readonly int _defaultTabIndex;
    private readonly global::Microsoft.UI.Xaml.Media.Media3D.Transform3D _defaultTransform3D;
    private readonly global::System.Numerics.Matrix4x4 _defaultTransformMatrix;
    private readonly global::Microsoft.UI.Xaml.Media.Animation.TransitionCollection _defaultTransitions;
    private readonly global::System.Numerics.Vector3 _defaultTranslation;
    private readonly global::Microsoft.UI.Xaml.Vector3Transition _defaultTranslationTransition;
    private readonly bool _defaultUseLayoutRounding;
    private readonly bool _defaultUseSystemFocusVisuals;
    private readonly global::Microsoft.UI.Xaml.Visibility _defaultVisibility;
    private readonly global::Microsoft.UI.Xaml.DependencyObject _defaultXYFocusDown;
    private readonly global::Microsoft.UI.Xaml.Input.XYFocusNavigationStrategy _defaultXYFocusDownNavigationStrategy;
    private readonly global::Microsoft.UI.Xaml.Input.XYFocusKeyboardNavigationMode _defaultXYFocusKeyboardNavigation;
    private readonly global::Microsoft.UI.Xaml.DependencyObject _defaultXYFocusLeft;
    private readonly global::Microsoft.UI.Xaml.Input.XYFocusNavigationStrategy _defaultXYFocusLeftNavigationStrategy;
    private readonly global::Microsoft.UI.Xaml.DependencyObject _defaultXYFocusRight;
    private readonly global::Microsoft.UI.Xaml.Input.XYFocusNavigationStrategy _defaultXYFocusRightNavigationStrategy;
    private readonly global::Microsoft.UI.Xaml.DependencyObject _defaultXYFocusUp;
    private readonly global::Microsoft.UI.Xaml.Input.XYFocusNavigationStrategy _defaultXYFocusUpNavigationStrategy;
    private readonly global::Microsoft.UI.Xaml.XamlRoot _defaultXamlRoot;
    private object? _configuredContent;
    private bool _hasRenderedChildren;

    public global::Microsoft.UI.Xaml.FrameworkElement Element => _control;

    public ButtonAdapter()
    {
        _defaultText = _control.Content;
        _defaultAutomationName = global::Microsoft.UI.Xaml.Automation.AutomationProperties.GetName(_control);
        _defaultToolTip = global::Microsoft.UI.Xaml.Controls.ToolTipService.GetToolTip(_control);
        _defaultIsEnabled = _control.IsEnabled;
        _defaultHorizontalAlignment = _control.HorizontalAlignment;
        _defaultVerticalAlignment = _control.VerticalAlignment;
        _defaultFlyout = _control.Flyout;
        _defaultClickMode = _control.ClickMode;
        _defaultCommand = _control.Command;
        _defaultCommandParameter = _control.CommandParameter;
        _defaultContentTemplate = _control.ContentTemplate;
        _defaultContentTemplateSelector = _control.ContentTemplateSelector;
        _defaultContentTransitions = _control.ContentTransitions;
        _defaultBackground = _control.Background;
        _defaultBackgroundSizing = _control.BackgroundSizing;
        _defaultBorderBrush = _control.BorderBrush;
        _defaultBorderThickness = _control.BorderThickness;
        _defaultCharacterSpacing = _control.CharacterSpacing;
        _defaultCornerRadius = _control.CornerRadius;
        _defaultDefaultStyleResourceUri = _control.DefaultStyleResourceUri;
        _defaultElementSoundMode = _control.ElementSoundMode;
        _defaultFontFamily = _control.FontFamily;
        _defaultFontSize = _control.FontSize;
        _defaultFontStretch = _control.FontStretch;
        _defaultFontStyle = _control.FontStyle;
        _defaultFontWeight = _control.FontWeight;
        _defaultForeground = _control.Foreground;
        _defaultHorizontalContentAlignment = _control.HorizontalContentAlignment;
        _defaultIsFocusEngaged = _control.IsFocusEngaged;
        _defaultIsFocusEngagementEnabled = _control.IsFocusEngagementEnabled;
        _defaultIsTextScaleFactorEnabled = _control.IsTextScaleFactorEnabled;
        _defaultPadding = _control.Padding;
        _defaultRequiresPointer = _control.RequiresPointer;
        _defaultTabNavigation = _control.TabNavigation;
        _defaultTemplate = _control.Template;
        _defaultVerticalContentAlignment = _control.VerticalContentAlignment;
        _defaultAllowFocusOnInteraction = _control.AllowFocusOnInteraction;
        _defaultAllowFocusWhenDisabled = _control.AllowFocusWhenDisabled;
        _defaultDataContext = _control.DataContext;
        _defaultFlowDirection = _control.FlowDirection;
        _defaultFocusVisualMargin = _control.FocusVisualMargin;
        _defaultFocusVisualPrimaryBrush = _control.FocusVisualPrimaryBrush;
        _defaultFocusVisualPrimaryThickness = _control.FocusVisualPrimaryThickness;
        _defaultFocusVisualSecondaryBrush = _control.FocusVisualSecondaryBrush;
        _defaultFocusVisualSecondaryThickness = _control.FocusVisualSecondaryThickness;
        _defaultHeight = _control.Height;
        _defaultLanguage = _control.Language;
        _defaultMargin = _control.Margin;
        _defaultMaxHeight = _control.MaxHeight;
        _defaultMaxWidth = _control.MaxWidth;
        _defaultMinHeight = _control.MinHeight;
        _defaultMinWidth = _control.MinWidth;
        _defaultName = _control.Name;
        _defaultRequestedTheme = _control.RequestedTheme;
        _defaultResources = _control.Resources;
        _defaultStyle = _control.Style;
        _defaultTag = _control.Tag;
        _defaultWidth = _control.Width;
        _defaultAccessKey = _control.AccessKey;
        _defaultAccessKeyScopeOwner = _control.AccessKeyScopeOwner;
        _defaultAllowDrop = _control.AllowDrop;
        _defaultCacheMode = _control.CacheMode;
        _defaultCanBeScrollAnchor = _control.CanBeScrollAnchor;
        _defaultCanDrag = _control.CanDrag;
        _defaultCenterPoint = _control.CenterPoint;
        _defaultClip = _control.Clip;
        _defaultCompositeMode = _control.CompositeMode;
        _defaultContextFlyout = _control.ContextFlyout;
        _defaultExitDisplayModeOnAccessKeyInvoked = _control.ExitDisplayModeOnAccessKeyInvoked;
        _defaultHighContrastAdjustment = _control.HighContrastAdjustment;
        _defaultIsAccessKeyScope = _control.IsAccessKeyScope;
        _defaultIsDoubleTapEnabled = _control.IsDoubleTapEnabled;
        _defaultIsHitTestVisible = _control.IsHitTestVisible;
        _defaultIsHoldingEnabled = _control.IsHoldingEnabled;
        _defaultIsRightTapEnabled = _control.IsRightTapEnabled;
        _defaultIsTabStop = _control.IsTabStop;
        _defaultIsTapEnabled = _control.IsTapEnabled;
        _defaultKeyTipHorizontalOffset = _control.KeyTipHorizontalOffset;
        _defaultKeyTipPlacementMode = _control.KeyTipPlacementMode;
        _defaultKeyTipTarget = _control.KeyTipTarget;
        _defaultKeyTipVerticalOffset = _control.KeyTipVerticalOffset;
        _defaultKeyboardAcceleratorPlacementMode = _control.KeyboardAcceleratorPlacementMode;
        _defaultKeyboardAcceleratorPlacementTarget = _control.KeyboardAcceleratorPlacementTarget;
        _defaultManipulationMode = _control.ManipulationMode;
        _defaultOpacity = _control.Opacity;
        _defaultOpacityTransition = _control.OpacityTransition;
        _defaultProjection = _control.Projection;
        _defaultRasterizationScale = _control.RasterizationScale;
        _defaultRenderTransform = _control.RenderTransform;
        _defaultRenderTransformOrigin = _control.RenderTransformOrigin;
        _defaultRotation = _control.Rotation;
        _defaultRotationAxis = _control.RotationAxis;
        _defaultRotationTransition = _control.RotationTransition;
        _defaultScale = _control.Scale;
        _defaultScaleTransition = _control.ScaleTransition;
        _defaultShadow = _control.Shadow;
        _defaultTabFocusNavigation = _control.TabFocusNavigation;
        _defaultTabIndex = _control.TabIndex;
        _defaultTransform3D = _control.Transform3D;
        _defaultTransformMatrix = _control.TransformMatrix;
        _defaultTransitions = _control.Transitions;
        _defaultTranslation = _control.Translation;
        _defaultTranslationTransition = _control.TranslationTransition;
        _defaultUseLayoutRounding = _control.UseLayoutRounding;
        _defaultUseSystemFocusVisuals = _control.UseSystemFocusVisuals;
        _defaultVisibility = _control.Visibility;
        _defaultXYFocusDown = _control.XYFocusDown;
        _defaultXYFocusDownNavigationStrategy = _control.XYFocusDownNavigationStrategy;
        _defaultXYFocusKeyboardNavigation = _control.XYFocusKeyboardNavigation;
        _defaultXYFocusLeft = _control.XYFocusLeft;
        _defaultXYFocusLeftNavigationStrategy = _control.XYFocusLeftNavigationStrategy;
        _defaultXYFocusRight = _control.XYFocusRight;
        _defaultXYFocusRightNavigationStrategy = _control.XYFocusRightNavigationStrategy;
        _defaultXYFocusUp = _control.XYFocusUp;
        _defaultXYFocusUpNavigationStrategy = _control.XYFocusUpNavigationStrategy;
        _defaultXamlRoot = _control.XamlRoot;
        _configuredContent = _defaultText;
        _control.Click += OnOnClick;
        _control.FocusDisengaged += OnOnFocusDisengaged;
        _control.FocusEngaged += OnOnFocusEngaged;
        _control.IsEnabledChanged += OnOnIsEnabledChanged;
        _control.ActualThemeChanged += OnOnActualThemeChanged;
        _control.DataContextChanged += OnOnDataContextChanged;
        _control.EffectiveViewportChanged += OnOnEffectiveViewportChanged;
        _control.LayoutUpdated += OnOnLayoutUpdated;
        _control.Loaded += OnOnLoaded;
        _control.Loading += OnOnLoading;
        _control.SizeChanged += OnOnSizeChanged;
        _control.Unloaded += OnOnUnloaded;
        _control.AccessKeyDisplayDismissed += OnOnAccessKeyDisplayDismissed;
        _control.AccessKeyDisplayRequested += OnOnAccessKeyDisplayRequested;
        _control.AccessKeyInvoked += OnOnAccessKeyInvoked;
        _control.BringIntoViewRequested += OnOnBringIntoViewRequested;
        _control.CharacterReceived += OnOnCharacterReceived;
        _control.ContextCanceled += OnOnContextCanceled;
        _control.ContextRequested += OnOnContextRequested;
        _control.DoubleTapped += OnOnDoubleTapped;
        _control.DragEnter += OnOnDragEnter;
        _control.DragLeave += OnOnDragLeave;
        _control.DragOver += OnOnDragOver;
        _control.DragStarting += OnOnDragStarting;
        _control.Drop += OnOnDrop;
        _control.DropCompleted += OnOnDropCompleted;
        _control.GettingFocus += OnOnGettingFocus;
        _control.GotFocus += OnOnGotFocus;
        _control.Holding += OnOnHolding;
        _control.KeyDown += OnOnKeyDown;
        _control.KeyUp += OnOnKeyUp;
        _control.LosingFocus += OnOnLosingFocus;
        _control.LostFocus += OnOnLostFocus;
        _control.ManipulationCompleted += OnOnManipulationCompleted;
        _control.ManipulationDelta += OnOnManipulationDelta;
        _control.ManipulationInertiaStarting += OnOnManipulationInertiaStarting;
        _control.ManipulationStarted += OnOnManipulationStarted;
        _control.ManipulationStarting += OnOnManipulationStarting;
        _control.NoFocusCandidateFound += OnOnNoFocusCandidateFound;
        _control.PointerCanceled += OnOnPointerCanceled;
        _control.PointerCaptureLost += OnOnPointerCaptureLost;
        _control.PointerEntered += OnOnPointerEntered;
        _control.PointerExited += OnOnPointerExited;
        _control.PointerMoved += OnOnPointerMoved;
        _control.PointerPressed += OnOnPointerPressed;
        _control.PointerReleased += OnOnPointerReleased;
        _control.PointerWheelChanged += OnOnPointerWheelChanged;
        _control.PreviewKeyDown += OnOnPreviewKeyDown;
        _control.PreviewKeyUp += OnOnPreviewKeyUp;
        _control.ProcessKeyboardAccelerators += OnOnProcessKeyboardAccelerators;
        _control.RightTapped += OnOnRightTapped;
        _control.Tapped += OnOnTapped;
    }

    public void ApplyParameters(global::Microsoft.AspNetCore.Components.ParameterView parameterView)
    {
        var currentParameters = new global::System.Collections.Generic.HashSet<string>(global::System.StringComparer.Ordinal);
        foreach (var parameter in parameterView)
        {
            currentParameters.Add(parameter.Name);
            switch (parameter.Name)
            {
                case "Text":
                {
                    _configuredContent = parameter.Value;
                    if (!_hasRenderedChildren) _control.Content = (object)parameter.Value!;
                    break;
                }
                case "AutomationName": global::Microsoft.UI.Xaml.Automation.AutomationProperties.SetName(_control, (string?)parameter.Value); break;
                case "ToolTip": global::Microsoft.UI.Xaml.Controls.ToolTipService.SetToolTip(_control, parameter.Value); break;
                case "IsEnabled": _control.IsEnabled = (bool)parameter.Value!; break;
                case "HorizontalAlignment": _control.HorizontalAlignment = (global::Microsoft.UI.Xaml.HorizontalAlignment)parameter.Value!; break;
                case "VerticalAlignment": _control.VerticalAlignment = (global::Microsoft.UI.Xaml.VerticalAlignment)parameter.Value!; break;
                case "Flyout": _control.Flyout = (global::Microsoft.UI.Xaml.Controls.Primitives.FlyoutBase)parameter.Value!; break;
                case "ClickMode": _control.ClickMode = (global::Microsoft.UI.Xaml.Controls.ClickMode)parameter.Value!; break;
                case "Command": _control.Command = (global::System.Windows.Input.ICommand)parameter.Value!; break;
                case "CommandParameter": _control.CommandParameter = (object)parameter.Value!; break;
                case "ContentTemplate": _control.ContentTemplate = (global::Microsoft.UI.Xaml.DataTemplate)parameter.Value!; break;
                case "ContentTemplateSelector": _control.ContentTemplateSelector = (global::Microsoft.UI.Xaml.Controls.DataTemplateSelector)parameter.Value!; break;
                case "ContentTransitions": _control.ContentTransitions = (global::Microsoft.UI.Xaml.Media.Animation.TransitionCollection)parameter.Value!; break;
                case "Background": _control.Background = (global::Microsoft.UI.Xaml.Media.Brush)parameter.Value!; break;
                case "BackgroundSizing": _control.BackgroundSizing = (global::Microsoft.UI.Xaml.Controls.BackgroundSizing)parameter.Value!; break;
                case "BorderBrush": _control.BorderBrush = (global::Microsoft.UI.Xaml.Media.Brush)parameter.Value!; break;
                case "BorderThickness": _control.BorderThickness = (global::Microsoft.UI.Xaml.Thickness)parameter.Value!; break;
                case "CharacterSpacing": _control.CharacterSpacing = (int)parameter.Value!; break;
                case "CornerRadius": _control.CornerRadius = (global::Microsoft.UI.Xaml.CornerRadius)parameter.Value!; break;
                case "DefaultStyleResourceUri": _control.DefaultStyleResourceUri = (global::System.Uri)parameter.Value!; break;
                case "ElementSoundMode": _control.ElementSoundMode = (global::Microsoft.UI.Xaml.ElementSoundMode)parameter.Value!; break;
                case "FontFamily": _control.FontFamily = (global::Microsoft.UI.Xaml.Media.FontFamily)parameter.Value!; break;
                case "FontSize": _control.FontSize = (double)parameter.Value!; break;
                case "FontStretch": _control.FontStretch = (global::Windows.UI.Text.FontStretch)parameter.Value!; break;
                case "FontStyle": _control.FontStyle = (global::Windows.UI.Text.FontStyle)parameter.Value!; break;
                case "FontWeight": _control.FontWeight = (global::Windows.UI.Text.FontWeight)parameter.Value!; break;
                case "Foreground": _control.Foreground = (global::Microsoft.UI.Xaml.Media.Brush)parameter.Value!; break;
                case "HorizontalContentAlignment": _control.HorizontalContentAlignment = (global::Microsoft.UI.Xaml.HorizontalAlignment)parameter.Value!; break;
                case "IsFocusEngaged": _control.IsFocusEngaged = (bool)parameter.Value!; break;
                case "IsFocusEngagementEnabled": _control.IsFocusEngagementEnabled = (bool)parameter.Value!; break;
                case "IsTextScaleFactorEnabled": _control.IsTextScaleFactorEnabled = (bool)parameter.Value!; break;
                case "Padding": _control.Padding = (global::Microsoft.UI.Xaml.Thickness)parameter.Value!; break;
                case "RequiresPointer": _control.RequiresPointer = (global::Microsoft.UI.Xaml.Controls.RequiresPointer)parameter.Value!; break;
                case "TabNavigation": _control.TabNavigation = (global::Microsoft.UI.Xaml.Input.KeyboardNavigationMode)parameter.Value!; break;
                case "Template": _control.Template = (global::Microsoft.UI.Xaml.Controls.ControlTemplate)parameter.Value!; break;
                case "VerticalContentAlignment": _control.VerticalContentAlignment = (global::Microsoft.UI.Xaml.VerticalAlignment)parameter.Value!; break;
                case "AllowFocusOnInteraction": _control.AllowFocusOnInteraction = (bool)parameter.Value!; break;
                case "AllowFocusWhenDisabled": _control.AllowFocusWhenDisabled = (bool)parameter.Value!; break;
                case "DataContext": _control.DataContext = (object)parameter.Value!; break;
                case "FlowDirection": _control.FlowDirection = (global::Microsoft.UI.Xaml.FlowDirection)parameter.Value!; break;
                case "FocusVisualMargin": _control.FocusVisualMargin = (global::Microsoft.UI.Xaml.Thickness)parameter.Value!; break;
                case "FocusVisualPrimaryBrush": _control.FocusVisualPrimaryBrush = (global::Microsoft.UI.Xaml.Media.Brush)parameter.Value!; break;
                case "FocusVisualPrimaryThickness": _control.FocusVisualPrimaryThickness = (global::Microsoft.UI.Xaml.Thickness)parameter.Value!; break;
                case "FocusVisualSecondaryBrush": _control.FocusVisualSecondaryBrush = (global::Microsoft.UI.Xaml.Media.Brush)parameter.Value!; break;
                case "FocusVisualSecondaryThickness": _control.FocusVisualSecondaryThickness = (global::Microsoft.UI.Xaml.Thickness)parameter.Value!; break;
                case "Height": _control.Height = (double)parameter.Value!; break;
                case "Language": _control.Language = (string)parameter.Value!; break;
                case "Margin": _control.Margin = (global::Microsoft.UI.Xaml.Thickness)parameter.Value!; break;
                case "MaxHeight": _control.MaxHeight = (double)parameter.Value!; break;
                case "MaxWidth": _control.MaxWidth = (double)parameter.Value!; break;
                case "MinHeight": _control.MinHeight = (double)parameter.Value!; break;
                case "MinWidth": _control.MinWidth = (double)parameter.Value!; break;
                case "Name": _control.Name = (string)parameter.Value!; break;
                case "RequestedTheme": _control.RequestedTheme = (global::Microsoft.UI.Xaml.ElementTheme)parameter.Value!; break;
                case "Resources": _control.Resources = (global::Microsoft.UI.Xaml.ResourceDictionary)parameter.Value!; break;
                case "Style": _control.Style = (global::Microsoft.UI.Xaml.Style)parameter.Value!; break;
                case "Tag": _control.Tag = (object)parameter.Value!; break;
                case "Width": _control.Width = (double)parameter.Value!; break;
                case "AccessKey": _control.AccessKey = (string)parameter.Value!; break;
                case "AccessKeyScopeOwner": _control.AccessKeyScopeOwner = (global::Microsoft.UI.Xaml.DependencyObject)parameter.Value!; break;
                case "AllowDrop": _control.AllowDrop = (bool)parameter.Value!; break;
                case "CacheMode": _control.CacheMode = (global::Microsoft.UI.Xaml.Media.CacheMode)parameter.Value!; break;
                case "CanBeScrollAnchor": _control.CanBeScrollAnchor = (bool)parameter.Value!; break;
                case "CanDrag": _control.CanDrag = (bool)parameter.Value!; break;
                case "CenterPoint": _control.CenterPoint = (global::System.Numerics.Vector3)parameter.Value!; break;
                case "Clip": _control.Clip = (global::Microsoft.UI.Xaml.Media.RectangleGeometry)parameter.Value!; break;
                case "CompositeMode": _control.CompositeMode = (global::Microsoft.UI.Xaml.Media.ElementCompositeMode)parameter.Value!; break;
                case "ContextFlyout": _control.ContextFlyout = (global::Microsoft.UI.Xaml.Controls.Primitives.FlyoutBase)parameter.Value!; break;
                case "ExitDisplayModeOnAccessKeyInvoked": _control.ExitDisplayModeOnAccessKeyInvoked = (bool)parameter.Value!; break;
                case "HighContrastAdjustment": _control.HighContrastAdjustment = (global::Microsoft.UI.Xaml.ElementHighContrastAdjustment)parameter.Value!; break;
                case "IsAccessKeyScope": _control.IsAccessKeyScope = (bool)parameter.Value!; break;
                case "IsDoubleTapEnabled": _control.IsDoubleTapEnabled = (bool)parameter.Value!; break;
                case "IsHitTestVisible": _control.IsHitTestVisible = (bool)parameter.Value!; break;
                case "IsHoldingEnabled": _control.IsHoldingEnabled = (bool)parameter.Value!; break;
                case "IsRightTapEnabled": _control.IsRightTapEnabled = (bool)parameter.Value!; break;
                case "IsTabStop": _control.IsTabStop = (bool)parameter.Value!; break;
                case "IsTapEnabled": _control.IsTapEnabled = (bool)parameter.Value!; break;
                case "KeyTipHorizontalOffset": _control.KeyTipHorizontalOffset = (double)parameter.Value!; break;
                case "KeyTipPlacementMode": _control.KeyTipPlacementMode = (global::Microsoft.UI.Xaml.Input.KeyTipPlacementMode)parameter.Value!; break;
                case "KeyTipTarget": _control.KeyTipTarget = (global::Microsoft.UI.Xaml.DependencyObject)parameter.Value!; break;
                case "KeyTipVerticalOffset": _control.KeyTipVerticalOffset = (double)parameter.Value!; break;
                case "KeyboardAcceleratorPlacementMode": _control.KeyboardAcceleratorPlacementMode = (global::Microsoft.UI.Xaml.Input.KeyboardAcceleratorPlacementMode)parameter.Value!; break;
                case "KeyboardAcceleratorPlacementTarget": _control.KeyboardAcceleratorPlacementTarget = (global::Microsoft.UI.Xaml.DependencyObject)parameter.Value!; break;
                case "ManipulationMode": _control.ManipulationMode = (global::Microsoft.UI.Xaml.Input.ManipulationModes)parameter.Value!; break;
                case "Opacity": _control.Opacity = (double)parameter.Value!; break;
                case "OpacityTransition": _control.OpacityTransition = (global::Microsoft.UI.Xaml.ScalarTransition)parameter.Value!; break;
                case "Projection": _control.Projection = (global::Microsoft.UI.Xaml.Media.Projection)parameter.Value!; break;
                case "RasterizationScale": _control.RasterizationScale = (double)parameter.Value!; break;
                case "RenderTransform": _control.RenderTransform = (global::Microsoft.UI.Xaml.Media.Transform)parameter.Value!; break;
                case "RenderTransformOrigin": _control.RenderTransformOrigin = (global::Windows.Foundation.Point)parameter.Value!; break;
                case "Rotation": _control.Rotation = (float)parameter.Value!; break;
                case "RotationAxis": _control.RotationAxis = (global::System.Numerics.Vector3)parameter.Value!; break;
                case "RotationTransition": _control.RotationTransition = (global::Microsoft.UI.Xaml.ScalarTransition)parameter.Value!; break;
                case "Scale": _control.Scale = (global::System.Numerics.Vector3)parameter.Value!; break;
                case "ScaleTransition": _control.ScaleTransition = (global::Microsoft.UI.Xaml.Vector3Transition)parameter.Value!; break;
                case "Shadow": _control.Shadow = (global::Microsoft.UI.Xaml.Media.Shadow)parameter.Value!; break;
                case "TabFocusNavigation": _control.TabFocusNavigation = (global::Microsoft.UI.Xaml.Input.KeyboardNavigationMode)parameter.Value!; break;
                case "TabIndex": _control.TabIndex = (int)parameter.Value!; break;
                case "Transform3D": _control.Transform3D = (global::Microsoft.UI.Xaml.Media.Media3D.Transform3D)parameter.Value!; break;
                case "TransformMatrix": _control.TransformMatrix = (global::System.Numerics.Matrix4x4)parameter.Value!; break;
                case "Transitions": _control.Transitions = (global::Microsoft.UI.Xaml.Media.Animation.TransitionCollection)parameter.Value!; break;
                case "Translation": _control.Translation = (global::System.Numerics.Vector3)parameter.Value!; break;
                case "TranslationTransition": _control.TranslationTransition = (global::Microsoft.UI.Xaml.Vector3Transition)parameter.Value!; break;
                case "UseLayoutRounding": _control.UseLayoutRounding = (bool)parameter.Value!; break;
                case "UseSystemFocusVisuals": _control.UseSystemFocusVisuals = (bool)parameter.Value!; break;
                case "Visibility": _control.Visibility = (global::Microsoft.UI.Xaml.Visibility)parameter.Value!; break;
                case "XYFocusDown": _control.XYFocusDown = (global::Microsoft.UI.Xaml.DependencyObject)parameter.Value!; break;
                case "XYFocusDownNavigationStrategy": _control.XYFocusDownNavigationStrategy = (global::Microsoft.UI.Xaml.Input.XYFocusNavigationStrategy)parameter.Value!; break;
                case "XYFocusKeyboardNavigation": _control.XYFocusKeyboardNavigation = (global::Microsoft.UI.Xaml.Input.XYFocusKeyboardNavigationMode)parameter.Value!; break;
                case "XYFocusLeft": _control.XYFocusLeft = (global::Microsoft.UI.Xaml.DependencyObject)parameter.Value!; break;
                case "XYFocusLeftNavigationStrategy": _control.XYFocusLeftNavigationStrategy = (global::Microsoft.UI.Xaml.Input.XYFocusNavigationStrategy)parameter.Value!; break;
                case "XYFocusRight": _control.XYFocusRight = (global::Microsoft.UI.Xaml.DependencyObject)parameter.Value!; break;
                case "XYFocusRightNavigationStrategy": _control.XYFocusRightNavigationStrategy = (global::Microsoft.UI.Xaml.Input.XYFocusNavigationStrategy)parameter.Value!; break;
                case "XYFocusUp": _control.XYFocusUp = (global::Microsoft.UI.Xaml.DependencyObject)parameter.Value!; break;
                case "XYFocusUpNavigationStrategy": _control.XYFocusUpNavigationStrategy = (global::Microsoft.UI.Xaml.Input.XYFocusNavigationStrategy)parameter.Value!; break;
                case "XamlRoot": _control.XamlRoot = (global::Microsoft.UI.Xaml.XamlRoot)parameter.Value!; break;
                case "OnClick": _onClick = (global::Microsoft.AspNetCore.Components.EventCallback)parameter.Value!; break;
                case "OnFocusDisengaged": _onFocusDisengaged = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Controls.FocusDisengagedEventArgs>)parameter.Value!; break;
                case "OnFocusEngaged": _onFocusEngaged = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Controls.FocusEngagedEventArgs>)parameter.Value!; break;
                case "OnIsEnabledChanged": _onIsEnabledChanged = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DependencyPropertyChangedEventArgs>)parameter.Value!; break;
                case "OnActualThemeChanged": _onActualThemeChanged = (global::Microsoft.AspNetCore.Components.EventCallback<object>)parameter.Value!; break;
                case "OnDataContextChanged": _onDataContextChanged = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DataContextChangedEventArgs>)parameter.Value!; break;
                case "OnEffectiveViewportChanged": _onEffectiveViewportChanged = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.EffectiveViewportChangedEventArgs>)parameter.Value!; break;
                case "OnLayoutUpdated": _onLayoutUpdated = (global::Microsoft.AspNetCore.Components.EventCallback<object>)parameter.Value!; break;
                case "OnLoaded": _onLoaded = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs>)parameter.Value!; break;
                case "OnLoading": _onLoading = (global::Microsoft.AspNetCore.Components.EventCallback<object>)parameter.Value!; break;
                case "OnSizeChanged": _onSizeChanged = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.SizeChangedEventArgs>)parameter.Value!; break;
                case "OnUnloaded": _onUnloaded = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs>)parameter.Value!; break;
                case "OnAccessKeyDisplayDismissed": _onAccessKeyDisplayDismissed = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.AccessKeyDisplayDismissedEventArgs>)parameter.Value!; break;
                case "OnAccessKeyDisplayRequested": _onAccessKeyDisplayRequested = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.AccessKeyDisplayRequestedEventArgs>)parameter.Value!; break;
                case "OnAccessKeyInvoked": _onAccessKeyInvoked = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.AccessKeyInvokedEventArgs>)parameter.Value!; break;
                case "OnBringIntoViewRequested": _onBringIntoViewRequested = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.BringIntoViewRequestedEventArgs>)parameter.Value!; break;
                case "OnCharacterReceived": _onCharacterReceived = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.CharacterReceivedRoutedEventArgs>)parameter.Value!; break;
                case "OnContextCanceled": _onContextCanceled = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs>)parameter.Value!; break;
                case "OnContextRequested": _onContextRequested = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ContextRequestedEventArgs>)parameter.Value!; break;
                case "OnDoubleTapped": _onDoubleTapped = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs>)parameter.Value!; break;
                case "OnDragEnter": _onDragEnter = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DragEventArgs>)parameter.Value!; break;
                case "OnDragLeave": _onDragLeave = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DragEventArgs>)parameter.Value!; break;
                case "OnDragOver": _onDragOver = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DragEventArgs>)parameter.Value!; break;
                case "OnDragStarting": _onDragStarting = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DragStartingEventArgs>)parameter.Value!; break;
                case "OnDrop": _onDrop = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DragEventArgs>)parameter.Value!; break;
                case "OnDropCompleted": _onDropCompleted = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DropCompletedEventArgs>)parameter.Value!; break;
                case "OnGettingFocus": _onGettingFocus = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.GettingFocusEventArgs>)parameter.Value!; break;
                case "OnGotFocus": _onGotFocus = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs>)parameter.Value!; break;
                case "OnHolding": _onHolding = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.HoldingRoutedEventArgs>)parameter.Value!; break;
                case "OnKeyDown": _onKeyDown = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs>)parameter.Value!; break;
                case "OnKeyUp": _onKeyUp = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs>)parameter.Value!; break;
                case "OnLosingFocus": _onLosingFocus = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.LosingFocusEventArgs>)parameter.Value!; break;
                case "OnLostFocus": _onLostFocus = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs>)parameter.Value!; break;
                case "OnManipulationCompleted": _onManipulationCompleted = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ManipulationCompletedRoutedEventArgs>)parameter.Value!; break;
                case "OnManipulationDelta": _onManipulationDelta = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ManipulationDeltaRoutedEventArgs>)parameter.Value!; break;
                case "OnManipulationInertiaStarting": _onManipulationInertiaStarting = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ManipulationInertiaStartingRoutedEventArgs>)parameter.Value!; break;
                case "OnManipulationStarted": _onManipulationStarted = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ManipulationStartedRoutedEventArgs>)parameter.Value!; break;
                case "OnManipulationStarting": _onManipulationStarting = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ManipulationStartingRoutedEventArgs>)parameter.Value!; break;
                case "OnNoFocusCandidateFound": _onNoFocusCandidateFound = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.NoFocusCandidateFoundEventArgs>)parameter.Value!; break;
                case "OnPointerCanceled": _onPointerCanceled = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs>)parameter.Value!; break;
                case "OnPointerCaptureLost": _onPointerCaptureLost = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs>)parameter.Value!; break;
                case "OnPointerEntered": _onPointerEntered = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs>)parameter.Value!; break;
                case "OnPointerExited": _onPointerExited = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs>)parameter.Value!; break;
                case "OnPointerMoved": _onPointerMoved = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs>)parameter.Value!; break;
                case "OnPointerPressed": _onPointerPressed = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs>)parameter.Value!; break;
                case "OnPointerReleased": _onPointerReleased = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs>)parameter.Value!; break;
                case "OnPointerWheelChanged": _onPointerWheelChanged = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs>)parameter.Value!; break;
                case "OnPreviewKeyDown": _onPreviewKeyDown = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs>)parameter.Value!; break;
                case "OnPreviewKeyUp": _onPreviewKeyUp = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs>)parameter.Value!; break;
                case "OnProcessKeyboardAccelerators": _onProcessKeyboardAccelerators = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ProcessKeyboardAcceleratorEventArgs>)parameter.Value!; break;
                case "OnRightTapped": _onRightTapped = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.RightTappedRoutedEventArgs>)parameter.Value!; break;
                case "OnTapped": _onTapped = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.TappedRoutedEventArgs>)parameter.Value!; break;
            }
        }
        foreach (var removedParameter in _presentParameters)
        {
            if (currentParameters.Contains(removedParameter)) continue;
            switch (removedParameter)
            {
                case "Text":
                {
                    _configuredContent = _defaultText;
                    if (!_hasRenderedChildren) _control.Content = _defaultText;
                    break;
                }
                case "AutomationName":
                {
                    global::Microsoft.UI.Xaml.Automation.AutomationProperties.SetName(_control, _defaultAutomationName);
                    break;
                }
                case "ToolTip":
                {
                    global::Microsoft.UI.Xaml.Controls.ToolTipService.SetToolTip(_control, _defaultToolTip);
                    break;
                }
                case "IsEnabled":
                {
                    _control.IsEnabled = _defaultIsEnabled;
                    break;
                }
                case "HorizontalAlignment":
                {
                    _control.HorizontalAlignment = _defaultHorizontalAlignment;
                    break;
                }
                case "VerticalAlignment":
                {
                    _control.VerticalAlignment = _defaultVerticalAlignment;
                    break;
                }
                case "Flyout":
                {
                    _control.Flyout = _defaultFlyout;
                    break;
                }
                case "ClickMode":
                {
                    _control.ClickMode = _defaultClickMode;
                    break;
                }
                case "Command":
                {
                    _control.Command = _defaultCommand;
                    break;
                }
                case "CommandParameter":
                {
                    _control.CommandParameter = _defaultCommandParameter;
                    break;
                }
                case "ContentTemplate":
                {
                    _control.ContentTemplate = _defaultContentTemplate;
                    break;
                }
                case "ContentTemplateSelector":
                {
                    _control.ContentTemplateSelector = _defaultContentTemplateSelector;
                    break;
                }
                case "ContentTransitions":
                {
                    _control.ContentTransitions = _defaultContentTransitions;
                    break;
                }
                case "Background":
                {
                    _control.Background = _defaultBackground;
                    break;
                }
                case "BackgroundSizing":
                {
                    _control.BackgroundSizing = _defaultBackgroundSizing;
                    break;
                }
                case "BorderBrush":
                {
                    _control.BorderBrush = _defaultBorderBrush;
                    break;
                }
                case "BorderThickness":
                {
                    _control.BorderThickness = _defaultBorderThickness;
                    break;
                }
                case "CharacterSpacing":
                {
                    _control.CharacterSpacing = _defaultCharacterSpacing;
                    break;
                }
                case "CornerRadius":
                {
                    _control.CornerRadius = _defaultCornerRadius;
                    break;
                }
                case "DefaultStyleResourceUri":
                {
                    _control.DefaultStyleResourceUri = _defaultDefaultStyleResourceUri;
                    break;
                }
                case "ElementSoundMode":
                {
                    _control.ElementSoundMode = _defaultElementSoundMode;
                    break;
                }
                case "FontFamily":
                {
                    _control.FontFamily = _defaultFontFamily;
                    break;
                }
                case "FontSize":
                {
                    _control.FontSize = _defaultFontSize;
                    break;
                }
                case "FontStretch":
                {
                    _control.FontStretch = _defaultFontStretch;
                    break;
                }
                case "FontStyle":
                {
                    _control.FontStyle = _defaultFontStyle;
                    break;
                }
                case "FontWeight":
                {
                    _control.FontWeight = _defaultFontWeight;
                    break;
                }
                case "Foreground":
                {
                    _control.Foreground = _defaultForeground;
                    break;
                }
                case "HorizontalContentAlignment":
                {
                    _control.HorizontalContentAlignment = _defaultHorizontalContentAlignment;
                    break;
                }
                case "IsFocusEngaged":
                {
                    _control.IsFocusEngaged = _defaultIsFocusEngaged;
                    break;
                }
                case "IsFocusEngagementEnabled":
                {
                    _control.IsFocusEngagementEnabled = _defaultIsFocusEngagementEnabled;
                    break;
                }
                case "IsTextScaleFactorEnabled":
                {
                    _control.IsTextScaleFactorEnabled = _defaultIsTextScaleFactorEnabled;
                    break;
                }
                case "Padding":
                {
                    _control.Padding = _defaultPadding;
                    break;
                }
                case "RequiresPointer":
                {
                    _control.RequiresPointer = _defaultRequiresPointer;
                    break;
                }
                case "TabNavigation":
                {
                    _control.TabNavigation = _defaultTabNavigation;
                    break;
                }
                case "Template":
                {
                    _control.Template = _defaultTemplate;
                    break;
                }
                case "VerticalContentAlignment":
                {
                    _control.VerticalContentAlignment = _defaultVerticalContentAlignment;
                    break;
                }
                case "AllowFocusOnInteraction":
                {
                    _control.AllowFocusOnInteraction = _defaultAllowFocusOnInteraction;
                    break;
                }
                case "AllowFocusWhenDisabled":
                {
                    _control.AllowFocusWhenDisabled = _defaultAllowFocusWhenDisabled;
                    break;
                }
                case "DataContext":
                {
                    _control.DataContext = _defaultDataContext;
                    break;
                }
                case "FlowDirection":
                {
                    _control.FlowDirection = _defaultFlowDirection;
                    break;
                }
                case "FocusVisualMargin":
                {
                    _control.FocusVisualMargin = _defaultFocusVisualMargin;
                    break;
                }
                case "FocusVisualPrimaryBrush":
                {
                    _control.FocusVisualPrimaryBrush = _defaultFocusVisualPrimaryBrush;
                    break;
                }
                case "FocusVisualPrimaryThickness":
                {
                    _control.FocusVisualPrimaryThickness = _defaultFocusVisualPrimaryThickness;
                    break;
                }
                case "FocusVisualSecondaryBrush":
                {
                    _control.FocusVisualSecondaryBrush = _defaultFocusVisualSecondaryBrush;
                    break;
                }
                case "FocusVisualSecondaryThickness":
                {
                    _control.FocusVisualSecondaryThickness = _defaultFocusVisualSecondaryThickness;
                    break;
                }
                case "Height":
                {
                    _control.Height = _defaultHeight;
                    break;
                }
                case "Language":
                {
                    _control.Language = _defaultLanguage;
                    break;
                }
                case "Margin":
                {
                    _control.Margin = _defaultMargin;
                    break;
                }
                case "MaxHeight":
                {
                    _control.MaxHeight = _defaultMaxHeight;
                    break;
                }
                case "MaxWidth":
                {
                    _control.MaxWidth = _defaultMaxWidth;
                    break;
                }
                case "MinHeight":
                {
                    _control.MinHeight = _defaultMinHeight;
                    break;
                }
                case "MinWidth":
                {
                    _control.MinWidth = _defaultMinWidth;
                    break;
                }
                case "Name":
                {
                    _control.Name = _defaultName;
                    break;
                }
                case "RequestedTheme":
                {
                    _control.RequestedTheme = _defaultRequestedTheme;
                    break;
                }
                case "Resources":
                {
                    _control.Resources = _defaultResources;
                    break;
                }
                case "Style":
                {
                    _control.Style = _defaultStyle;
                    break;
                }
                case "Tag":
                {
                    _control.Tag = _defaultTag;
                    break;
                }
                case "Width":
                {
                    _control.Width = _defaultWidth;
                    break;
                }
                case "AccessKey":
                {
                    _control.AccessKey = _defaultAccessKey;
                    break;
                }
                case "AccessKeyScopeOwner":
                {
                    _control.AccessKeyScopeOwner = _defaultAccessKeyScopeOwner;
                    break;
                }
                case "AllowDrop":
                {
                    _control.AllowDrop = _defaultAllowDrop;
                    break;
                }
                case "CacheMode":
                {
                    _control.CacheMode = _defaultCacheMode;
                    break;
                }
                case "CanBeScrollAnchor":
                {
                    _control.CanBeScrollAnchor = _defaultCanBeScrollAnchor;
                    break;
                }
                case "CanDrag":
                {
                    _control.CanDrag = _defaultCanDrag;
                    break;
                }
                case "CenterPoint":
                {
                    _control.CenterPoint = _defaultCenterPoint;
                    break;
                }
                case "Clip":
                {
                    _control.Clip = _defaultClip;
                    break;
                }
                case "CompositeMode":
                {
                    _control.CompositeMode = _defaultCompositeMode;
                    break;
                }
                case "ContextFlyout":
                {
                    _control.ContextFlyout = _defaultContextFlyout;
                    break;
                }
                case "ExitDisplayModeOnAccessKeyInvoked":
                {
                    _control.ExitDisplayModeOnAccessKeyInvoked = _defaultExitDisplayModeOnAccessKeyInvoked;
                    break;
                }
                case "HighContrastAdjustment":
                {
                    _control.HighContrastAdjustment = _defaultHighContrastAdjustment;
                    break;
                }
                case "IsAccessKeyScope":
                {
                    _control.IsAccessKeyScope = _defaultIsAccessKeyScope;
                    break;
                }
                case "IsDoubleTapEnabled":
                {
                    _control.IsDoubleTapEnabled = _defaultIsDoubleTapEnabled;
                    break;
                }
                case "IsHitTestVisible":
                {
                    _control.IsHitTestVisible = _defaultIsHitTestVisible;
                    break;
                }
                case "IsHoldingEnabled":
                {
                    _control.IsHoldingEnabled = _defaultIsHoldingEnabled;
                    break;
                }
                case "IsRightTapEnabled":
                {
                    _control.IsRightTapEnabled = _defaultIsRightTapEnabled;
                    break;
                }
                case "IsTabStop":
                {
                    _control.IsTabStop = _defaultIsTabStop;
                    break;
                }
                case "IsTapEnabled":
                {
                    _control.IsTapEnabled = _defaultIsTapEnabled;
                    break;
                }
                case "KeyTipHorizontalOffset":
                {
                    _control.KeyTipHorizontalOffset = _defaultKeyTipHorizontalOffset;
                    break;
                }
                case "KeyTipPlacementMode":
                {
                    _control.KeyTipPlacementMode = _defaultKeyTipPlacementMode;
                    break;
                }
                case "KeyTipTarget":
                {
                    _control.KeyTipTarget = _defaultKeyTipTarget;
                    break;
                }
                case "KeyTipVerticalOffset":
                {
                    _control.KeyTipVerticalOffset = _defaultKeyTipVerticalOffset;
                    break;
                }
                case "KeyboardAcceleratorPlacementMode":
                {
                    _control.KeyboardAcceleratorPlacementMode = _defaultKeyboardAcceleratorPlacementMode;
                    break;
                }
                case "KeyboardAcceleratorPlacementTarget":
                {
                    _control.KeyboardAcceleratorPlacementTarget = _defaultKeyboardAcceleratorPlacementTarget;
                    break;
                }
                case "ManipulationMode":
                {
                    _control.ManipulationMode = _defaultManipulationMode;
                    break;
                }
                case "Opacity":
                {
                    _control.Opacity = _defaultOpacity;
                    break;
                }
                case "OpacityTransition":
                {
                    _control.OpacityTransition = _defaultOpacityTransition;
                    break;
                }
                case "Projection":
                {
                    _control.Projection = _defaultProjection;
                    break;
                }
                case "RasterizationScale":
                {
                    _control.RasterizationScale = _defaultRasterizationScale;
                    break;
                }
                case "RenderTransform":
                {
                    _control.RenderTransform = _defaultRenderTransform;
                    break;
                }
                case "RenderTransformOrigin":
                {
                    _control.RenderTransformOrigin = _defaultRenderTransformOrigin;
                    break;
                }
                case "Rotation":
                {
                    _control.Rotation = _defaultRotation;
                    break;
                }
                case "RotationAxis":
                {
                    _control.RotationAxis = _defaultRotationAxis;
                    break;
                }
                case "RotationTransition":
                {
                    _control.RotationTransition = _defaultRotationTransition;
                    break;
                }
                case "Scale":
                {
                    _control.Scale = _defaultScale;
                    break;
                }
                case "ScaleTransition":
                {
                    _control.ScaleTransition = _defaultScaleTransition;
                    break;
                }
                case "Shadow":
                {
                    _control.Shadow = _defaultShadow;
                    break;
                }
                case "TabFocusNavigation":
                {
                    _control.TabFocusNavigation = _defaultTabFocusNavigation;
                    break;
                }
                case "TabIndex":
                {
                    _control.TabIndex = _defaultTabIndex;
                    break;
                }
                case "Transform3D":
                {
                    _control.Transform3D = _defaultTransform3D;
                    break;
                }
                case "TransformMatrix":
                {
                    _control.TransformMatrix = _defaultTransformMatrix;
                    break;
                }
                case "Transitions":
                {
                    _control.Transitions = _defaultTransitions;
                    break;
                }
                case "Translation":
                {
                    _control.Translation = _defaultTranslation;
                    break;
                }
                case "TranslationTransition":
                {
                    _control.TranslationTransition = _defaultTranslationTransition;
                    break;
                }
                case "UseLayoutRounding":
                {
                    _control.UseLayoutRounding = _defaultUseLayoutRounding;
                    break;
                }
                case "UseSystemFocusVisuals":
                {
                    _control.UseSystemFocusVisuals = _defaultUseSystemFocusVisuals;
                    break;
                }
                case "Visibility":
                {
                    _control.Visibility = _defaultVisibility;
                    break;
                }
                case "XYFocusDown":
                {
                    _control.XYFocusDown = _defaultXYFocusDown;
                    break;
                }
                case "XYFocusDownNavigationStrategy":
                {
                    _control.XYFocusDownNavigationStrategy = _defaultXYFocusDownNavigationStrategy;
                    break;
                }
                case "XYFocusKeyboardNavigation":
                {
                    _control.XYFocusKeyboardNavigation = _defaultXYFocusKeyboardNavigation;
                    break;
                }
                case "XYFocusLeft":
                {
                    _control.XYFocusLeft = _defaultXYFocusLeft;
                    break;
                }
                case "XYFocusLeftNavigationStrategy":
                {
                    _control.XYFocusLeftNavigationStrategy = _defaultXYFocusLeftNavigationStrategy;
                    break;
                }
                case "XYFocusRight":
                {
                    _control.XYFocusRight = _defaultXYFocusRight;
                    break;
                }
                case "XYFocusRightNavigationStrategy":
                {
                    _control.XYFocusRightNavigationStrategy = _defaultXYFocusRightNavigationStrategy;
                    break;
                }
                case "XYFocusUp":
                {
                    _control.XYFocusUp = _defaultXYFocusUp;
                    break;
                }
                case "XYFocusUpNavigationStrategy":
                {
                    _control.XYFocusUpNavigationStrategy = _defaultXYFocusUpNavigationStrategy;
                    break;
                }
                case "XamlRoot":
                {
                    _control.XamlRoot = _defaultXamlRoot;
                    break;
                }
                case "OnClick": _onClick = default; break;
                case "OnFocusDisengaged": _onFocusDisengaged = default; break;
                case "OnFocusEngaged": _onFocusEngaged = default; break;
                case "OnIsEnabledChanged": _onIsEnabledChanged = default; break;
                case "OnActualThemeChanged": _onActualThemeChanged = default; break;
                case "OnDataContextChanged": _onDataContextChanged = default; break;
                case "OnEffectiveViewportChanged": _onEffectiveViewportChanged = default; break;
                case "OnLayoutUpdated": _onLayoutUpdated = default; break;
                case "OnLoaded": _onLoaded = default; break;
                case "OnLoading": _onLoading = default; break;
                case "OnSizeChanged": _onSizeChanged = default; break;
                case "OnUnloaded": _onUnloaded = default; break;
                case "OnAccessKeyDisplayDismissed": _onAccessKeyDisplayDismissed = default; break;
                case "OnAccessKeyDisplayRequested": _onAccessKeyDisplayRequested = default; break;
                case "OnAccessKeyInvoked": _onAccessKeyInvoked = default; break;
                case "OnBringIntoViewRequested": _onBringIntoViewRequested = default; break;
                case "OnCharacterReceived": _onCharacterReceived = default; break;
                case "OnContextCanceled": _onContextCanceled = default; break;
                case "OnContextRequested": _onContextRequested = default; break;
                case "OnDoubleTapped": _onDoubleTapped = default; break;
                case "OnDragEnter": _onDragEnter = default; break;
                case "OnDragLeave": _onDragLeave = default; break;
                case "OnDragOver": _onDragOver = default; break;
                case "OnDragStarting": _onDragStarting = default; break;
                case "OnDrop": _onDrop = default; break;
                case "OnDropCompleted": _onDropCompleted = default; break;
                case "OnGettingFocus": _onGettingFocus = default; break;
                case "OnGotFocus": _onGotFocus = default; break;
                case "OnHolding": _onHolding = default; break;
                case "OnKeyDown": _onKeyDown = default; break;
                case "OnKeyUp": _onKeyUp = default; break;
                case "OnLosingFocus": _onLosingFocus = default; break;
                case "OnLostFocus": _onLostFocus = default; break;
                case "OnManipulationCompleted": _onManipulationCompleted = default; break;
                case "OnManipulationDelta": _onManipulationDelta = default; break;
                case "OnManipulationInertiaStarting": _onManipulationInertiaStarting = default; break;
                case "OnManipulationStarted": _onManipulationStarted = default; break;
                case "OnManipulationStarting": _onManipulationStarting = default; break;
                case "OnNoFocusCandidateFound": _onNoFocusCandidateFound = default; break;
                case "OnPointerCanceled": _onPointerCanceled = default; break;
                case "OnPointerCaptureLost": _onPointerCaptureLost = default; break;
                case "OnPointerEntered": _onPointerEntered = default; break;
                case "OnPointerExited": _onPointerExited = default; break;
                case "OnPointerMoved": _onPointerMoved = default; break;
                case "OnPointerPressed": _onPointerPressed = default; break;
                case "OnPointerReleased": _onPointerReleased = default; break;
                case "OnPointerWheelChanged": _onPointerWheelChanged = default; break;
                case "OnPreviewKeyDown": _onPreviewKeyDown = default; break;
                case "OnPreviewKeyUp": _onPreviewKeyUp = default; break;
                case "OnProcessKeyboardAccelerators": _onProcessKeyboardAccelerators = default; break;
                case "OnRightTapped": _onRightTapped = default; break;
                case "OnTapped": _onTapped = default; break;
            }
        }
        _presentParameters.Clear();
        _presentParameters.UnionWith(currentParameters);
    }

    private async void OnOnClick(object sender, global::Microsoft.UI.Xaml.RoutedEventArgs args)
    {
        await _onClick.InvokeAsync();
    }

    private async void OnOnFocusDisengaged(global::Microsoft.UI.Xaml.Controls.Control arg0, global::Microsoft.UI.Xaml.Controls.FocusDisengagedEventArgs arg1)
    {
        await _onFocusDisengaged.InvokeAsync(arg1);
    }

    private async void OnOnFocusEngaged(global::Microsoft.UI.Xaml.Controls.Control arg0, global::Microsoft.UI.Xaml.Controls.FocusEngagedEventArgs arg1)
    {
        await _onFocusEngaged.InvokeAsync(arg1);
    }

    private async void OnOnIsEnabledChanged(object arg0, global::Microsoft.UI.Xaml.DependencyPropertyChangedEventArgs arg1)
    {
        await _onIsEnabledChanged.InvokeAsync(arg1);
    }

    private async void OnOnActualThemeChanged(global::Microsoft.UI.Xaml.FrameworkElement arg0, object arg1)
    {
        await _onActualThemeChanged.InvokeAsync(arg1);
    }

    private async void OnOnDataContextChanged(global::Microsoft.UI.Xaml.FrameworkElement arg0, global::Microsoft.UI.Xaml.DataContextChangedEventArgs arg1)
    {
        await _onDataContextChanged.InvokeAsync(arg1);
    }

    private async void OnOnEffectiveViewportChanged(global::Microsoft.UI.Xaml.FrameworkElement arg0, global::Microsoft.UI.Xaml.EffectiveViewportChangedEventArgs arg1)
    {
        await _onEffectiveViewportChanged.InvokeAsync(arg1);
    }

    private async void OnOnLayoutUpdated(object? arg0, object arg1)
    {
        await _onLayoutUpdated.InvokeAsync(arg1);
    }

    private async void OnOnLoaded(object arg0, global::Microsoft.UI.Xaml.RoutedEventArgs arg1)
    {
        await _onLoaded.InvokeAsync(arg1);
    }

    private async void OnOnLoading(global::Microsoft.UI.Xaml.FrameworkElement arg0, object arg1)
    {
        await _onLoading.InvokeAsync(arg1);
    }

    private async void OnOnSizeChanged(object arg0, global::Microsoft.UI.Xaml.SizeChangedEventArgs arg1)
    {
        await _onSizeChanged.InvokeAsync(arg1);
    }

    private async void OnOnUnloaded(object arg0, global::Microsoft.UI.Xaml.RoutedEventArgs arg1)
    {
        await _onUnloaded.InvokeAsync(arg1);
    }

    private async void OnOnAccessKeyDisplayDismissed(global::Microsoft.UI.Xaml.UIElement arg0, global::Microsoft.UI.Xaml.Input.AccessKeyDisplayDismissedEventArgs arg1)
    {
        await _onAccessKeyDisplayDismissed.InvokeAsync(arg1);
    }

    private async void OnOnAccessKeyDisplayRequested(global::Microsoft.UI.Xaml.UIElement arg0, global::Microsoft.UI.Xaml.Input.AccessKeyDisplayRequestedEventArgs arg1)
    {
        await _onAccessKeyDisplayRequested.InvokeAsync(arg1);
    }

    private async void OnOnAccessKeyInvoked(global::Microsoft.UI.Xaml.UIElement arg0, global::Microsoft.UI.Xaml.Input.AccessKeyInvokedEventArgs arg1)
    {
        await _onAccessKeyInvoked.InvokeAsync(arg1);
    }

    private async void OnOnBringIntoViewRequested(global::Microsoft.UI.Xaml.UIElement arg0, global::Microsoft.UI.Xaml.BringIntoViewRequestedEventArgs arg1)
    {
        await _onBringIntoViewRequested.InvokeAsync(arg1);
    }

    private async void OnOnCharacterReceived(global::Microsoft.UI.Xaml.UIElement arg0, global::Microsoft.UI.Xaml.Input.CharacterReceivedRoutedEventArgs arg1)
    {
        await _onCharacterReceived.InvokeAsync(arg1);
    }

    private async void OnOnContextCanceled(global::Microsoft.UI.Xaml.UIElement arg0, global::Microsoft.UI.Xaml.RoutedEventArgs arg1)
    {
        await _onContextCanceled.InvokeAsync(arg1);
    }

    private async void OnOnContextRequested(global::Microsoft.UI.Xaml.UIElement arg0, global::Microsoft.UI.Xaml.Input.ContextRequestedEventArgs arg1)
    {
        await _onContextRequested.InvokeAsync(arg1);
    }

    private async void OnOnDoubleTapped(object arg0, global::Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs arg1)
    {
        await _onDoubleTapped.InvokeAsync(arg1);
    }

    private async void OnOnDragEnter(object arg0, global::Microsoft.UI.Xaml.DragEventArgs arg1)
    {
        await _onDragEnter.InvokeAsync(arg1);
    }

    private async void OnOnDragLeave(object arg0, global::Microsoft.UI.Xaml.DragEventArgs arg1)
    {
        await _onDragLeave.InvokeAsync(arg1);
    }

    private async void OnOnDragOver(object arg0, global::Microsoft.UI.Xaml.DragEventArgs arg1)
    {
        await _onDragOver.InvokeAsync(arg1);
    }

    private async void OnOnDragStarting(global::Microsoft.UI.Xaml.UIElement arg0, global::Microsoft.UI.Xaml.DragStartingEventArgs arg1)
    {
        await _onDragStarting.InvokeAsync(arg1);
    }

    private async void OnOnDrop(object arg0, global::Microsoft.UI.Xaml.DragEventArgs arg1)
    {
        await _onDrop.InvokeAsync(arg1);
    }

    private async void OnOnDropCompleted(global::Microsoft.UI.Xaml.UIElement arg0, global::Microsoft.UI.Xaml.DropCompletedEventArgs arg1)
    {
        await _onDropCompleted.InvokeAsync(arg1);
    }

    private async void OnOnGettingFocus(global::Microsoft.UI.Xaml.UIElement arg0, global::Microsoft.UI.Xaml.Input.GettingFocusEventArgs arg1)
    {
        await _onGettingFocus.InvokeAsync(arg1);
    }

    private async void OnOnGotFocus(object arg0, global::Microsoft.UI.Xaml.RoutedEventArgs arg1)
    {
        await _onGotFocus.InvokeAsync(arg1);
    }

    private async void OnOnHolding(object arg0, global::Microsoft.UI.Xaml.Input.HoldingRoutedEventArgs arg1)
    {
        await _onHolding.InvokeAsync(arg1);
    }

    private async void OnOnKeyDown(object arg0, global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs arg1)
    {
        await _onKeyDown.InvokeAsync(arg1);
    }

    private async void OnOnKeyUp(object arg0, global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs arg1)
    {
        await _onKeyUp.InvokeAsync(arg1);
    }

    private async void OnOnLosingFocus(global::Microsoft.UI.Xaml.UIElement arg0, global::Microsoft.UI.Xaml.Input.LosingFocusEventArgs arg1)
    {
        await _onLosingFocus.InvokeAsync(arg1);
    }

    private async void OnOnLostFocus(object arg0, global::Microsoft.UI.Xaml.RoutedEventArgs arg1)
    {
        await _onLostFocus.InvokeAsync(arg1);
    }

    private async void OnOnManipulationCompleted(object arg0, global::Microsoft.UI.Xaml.Input.ManipulationCompletedRoutedEventArgs arg1)
    {
        await _onManipulationCompleted.InvokeAsync(arg1);
    }

    private async void OnOnManipulationDelta(object arg0, global::Microsoft.UI.Xaml.Input.ManipulationDeltaRoutedEventArgs arg1)
    {
        await _onManipulationDelta.InvokeAsync(arg1);
    }

    private async void OnOnManipulationInertiaStarting(object arg0, global::Microsoft.UI.Xaml.Input.ManipulationInertiaStartingRoutedEventArgs arg1)
    {
        await _onManipulationInertiaStarting.InvokeAsync(arg1);
    }

    private async void OnOnManipulationStarted(object arg0, global::Microsoft.UI.Xaml.Input.ManipulationStartedRoutedEventArgs arg1)
    {
        await _onManipulationStarted.InvokeAsync(arg1);
    }

    private async void OnOnManipulationStarting(object arg0, global::Microsoft.UI.Xaml.Input.ManipulationStartingRoutedEventArgs arg1)
    {
        await _onManipulationStarting.InvokeAsync(arg1);
    }

    private async void OnOnNoFocusCandidateFound(global::Microsoft.UI.Xaml.UIElement arg0, global::Microsoft.UI.Xaml.Input.NoFocusCandidateFoundEventArgs arg1)
    {
        await _onNoFocusCandidateFound.InvokeAsync(arg1);
    }

    private async void OnOnPointerCanceled(object arg0, global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs arg1)
    {
        await _onPointerCanceled.InvokeAsync(arg1);
    }

    private async void OnOnPointerCaptureLost(object arg0, global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs arg1)
    {
        await _onPointerCaptureLost.InvokeAsync(arg1);
    }

    private async void OnOnPointerEntered(object arg0, global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs arg1)
    {
        await _onPointerEntered.InvokeAsync(arg1);
    }

    private async void OnOnPointerExited(object arg0, global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs arg1)
    {
        await _onPointerExited.InvokeAsync(arg1);
    }

    private async void OnOnPointerMoved(object arg0, global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs arg1)
    {
        await _onPointerMoved.InvokeAsync(arg1);
    }

    private async void OnOnPointerPressed(object arg0, global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs arg1)
    {
        await _onPointerPressed.InvokeAsync(arg1);
    }

    private async void OnOnPointerReleased(object arg0, global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs arg1)
    {
        await _onPointerReleased.InvokeAsync(arg1);
    }

    private async void OnOnPointerWheelChanged(object arg0, global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs arg1)
    {
        await _onPointerWheelChanged.InvokeAsync(arg1);
    }

    private async void OnOnPreviewKeyDown(object arg0, global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs arg1)
    {
        await _onPreviewKeyDown.InvokeAsync(arg1);
    }

    private async void OnOnPreviewKeyUp(object arg0, global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs arg1)
    {
        await _onPreviewKeyUp.InvokeAsync(arg1);
    }

    private async void OnOnProcessKeyboardAccelerators(global::Microsoft.UI.Xaml.UIElement arg0, global::Microsoft.UI.Xaml.Input.ProcessKeyboardAcceleratorEventArgs arg1)
    {
        await _onProcessKeyboardAccelerators.InvokeAsync(arg1);
    }

    private async void OnOnRightTapped(object arg0, global::Microsoft.UI.Xaml.Input.RightTappedRoutedEventArgs arg1)
    {
        await _onRightTapped.InvokeAsync(arg1);
    }

    private async void OnOnTapped(object arg0, global::Microsoft.UI.Xaml.Input.TappedRoutedEventArgs arg1)
    {
        await _onTapped.InvokeAsync(arg1);
    }

    public void SetChildren(global::System.Collections.Generic.IReadOnlyList<global::Microsoft.UI.Xaml.FrameworkElement> children)
    {
        if (children.Count > 1)
        {
            throw new global::System.InvalidOperationException("This WinUI control accepts at most one child component.");
        }
        _hasRenderedChildren = children.Count > 0;
        _control.Content = children.Count == 0 ? (object)_configuredContent! : children[0];
    }

    public void Dispose()
    {
        _control.Click -= OnOnClick;
        _control.FocusDisengaged -= OnOnFocusDisengaged;
        _control.FocusEngaged -= OnOnFocusEngaged;
        _control.IsEnabledChanged -= OnOnIsEnabledChanged;
        _control.ActualThemeChanged -= OnOnActualThemeChanged;
        _control.DataContextChanged -= OnOnDataContextChanged;
        _control.EffectiveViewportChanged -= OnOnEffectiveViewportChanged;
        _control.LayoutUpdated -= OnOnLayoutUpdated;
        _control.Loaded -= OnOnLoaded;
        _control.Loading -= OnOnLoading;
        _control.SizeChanged -= OnOnSizeChanged;
        _control.Unloaded -= OnOnUnloaded;
        _control.AccessKeyDisplayDismissed -= OnOnAccessKeyDisplayDismissed;
        _control.AccessKeyDisplayRequested -= OnOnAccessKeyDisplayRequested;
        _control.AccessKeyInvoked -= OnOnAccessKeyInvoked;
        _control.BringIntoViewRequested -= OnOnBringIntoViewRequested;
        _control.CharacterReceived -= OnOnCharacterReceived;
        _control.ContextCanceled -= OnOnContextCanceled;
        _control.ContextRequested -= OnOnContextRequested;
        _control.DoubleTapped -= OnOnDoubleTapped;
        _control.DragEnter -= OnOnDragEnter;
        _control.DragLeave -= OnOnDragLeave;
        _control.DragOver -= OnOnDragOver;
        _control.DragStarting -= OnOnDragStarting;
        _control.Drop -= OnOnDrop;
        _control.DropCompleted -= OnOnDropCompleted;
        _control.GettingFocus -= OnOnGettingFocus;
        _control.GotFocus -= OnOnGotFocus;
        _control.Holding -= OnOnHolding;
        _control.KeyDown -= OnOnKeyDown;
        _control.KeyUp -= OnOnKeyUp;
        _control.LosingFocus -= OnOnLosingFocus;
        _control.LostFocus -= OnOnLostFocus;
        _control.ManipulationCompleted -= OnOnManipulationCompleted;
        _control.ManipulationDelta -= OnOnManipulationDelta;
        _control.ManipulationInertiaStarting -= OnOnManipulationInertiaStarting;
        _control.ManipulationStarted -= OnOnManipulationStarted;
        _control.ManipulationStarting -= OnOnManipulationStarting;
        _control.NoFocusCandidateFound -= OnOnNoFocusCandidateFound;
        _control.PointerCanceled -= OnOnPointerCanceled;
        _control.PointerCaptureLost -= OnOnPointerCaptureLost;
        _control.PointerEntered -= OnOnPointerEntered;
        _control.PointerExited -= OnOnPointerExited;
        _control.PointerMoved -= OnOnPointerMoved;
        _control.PointerPressed -= OnOnPointerPressed;
        _control.PointerReleased -= OnOnPointerReleased;
        _control.PointerWheelChanged -= OnOnPointerWheelChanged;
        _control.PreviewKeyDown -= OnOnPreviewKeyDown;
        _control.PreviewKeyUp -= OnOnPreviewKeyUp;
        _control.ProcessKeyboardAccelerators -= OnOnProcessKeyboardAccelerators;
        _control.RightTapped -= OnOnRightTapped;
        _control.Tapped -= OnOnTapped;
    }
}
