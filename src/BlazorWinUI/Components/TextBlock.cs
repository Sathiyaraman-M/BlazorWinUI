#nullable enable
namespace BlazorWinUI.Components;

public sealed class TextBlock : global::Microsoft.AspNetCore.Components.ComponentBase
{
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public string Text { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public double FontSize { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.FontFamily FontFamily { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Windows.UI.Text.FontWeight FontWeight { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.Brush Foreground { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.TextAlignment TextAlignment { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.TextWrapping TextWrapping { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.TextTrimming TextTrimming { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool IsTextSelectionEnabled { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public int MaxLines { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public int CharacterSpacing { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Windows.UI.Text.FontStretch FontStretch { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Windows.UI.Text.FontStyle FontStyle { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.TextAlignment HorizontalTextAlignment { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool IsColorFontEnabled { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool IsTextScaleFactorEnabled { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public double LineHeight { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.LineStackingStrategy LineStackingStrategy { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.OpticalMarginAlignment OpticalMarginAlignment { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Thickness Padding { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Controls.Primitives.FlyoutBase SelectionFlyout { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.SolidColorBrush SelectionHighlightColor { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Windows.UI.Text.TextDecorations TextDecorations { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.TextLineBounds TextLineBounds { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.TextReadingOrder TextReadingOrder { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool AllowFocusOnInteraction { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool AllowFocusWhenDisabled { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public object DataContext { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.FlowDirection FlowDirection { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Thickness FocusVisualMargin { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.Brush FocusVisualPrimaryBrush { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Thickness FocusVisualPrimaryThickness { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.Brush FocusVisualSecondaryBrush { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Thickness FocusVisualSecondaryThickness { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public double Height { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.HorizontalAlignment HorizontalAlignment { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public string Language { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Thickness Margin { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public double MaxHeight { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public double MaxWidth { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public double MinHeight { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public double MinWidth { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public string Name { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.ElementTheme RequestedTheme { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.ResourceDictionary Resources { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Style Style { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public object Tag { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.VerticalAlignment VerticalAlignment { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public double Width { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public string AccessKey { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.DependencyObject AccessKeyScopeOwner { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool AllowDrop { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.CacheMode CacheMode { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool CanBeScrollAnchor { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool CanDrag { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::System.Numerics.Vector3 CenterPoint { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.RectangleGeometry Clip { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.ElementCompositeMode CompositeMode { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Controls.Primitives.FlyoutBase ContextFlyout { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool ExitDisplayModeOnAccessKeyInvoked { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.ElementHighContrastAdjustment HighContrastAdjustment { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool IsAccessKeyScope { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool IsDoubleTapEnabled { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool IsHitTestVisible { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool IsHoldingEnabled { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool IsRightTapEnabled { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool IsTabStop { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool IsTapEnabled { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public double KeyTipHorizontalOffset { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Input.KeyTipPlacementMode KeyTipPlacementMode { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.DependencyObject KeyTipTarget { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public double KeyTipVerticalOffset { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Input.KeyboardAcceleratorPlacementMode KeyboardAcceleratorPlacementMode { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.DependencyObject KeyboardAcceleratorPlacementTarget { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Input.ManipulationModes ManipulationMode { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public double Opacity { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.ScalarTransition OpacityTransition { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.Projection Projection { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public double RasterizationScale { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.Transform RenderTransform { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Windows.Foundation.Point RenderTransformOrigin { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public float Rotation { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::System.Numerics.Vector3 RotationAxis { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.ScalarTransition RotationTransition { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::System.Numerics.Vector3 Scale { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Vector3Transition ScaleTransition { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.Shadow Shadow { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Input.KeyboardNavigationMode TabFocusNavigation { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public int TabIndex { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.Media3D.Transform3D Transform3D { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::System.Numerics.Matrix4x4 TransformMatrix { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.Animation.TransitionCollection Transitions { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::System.Numerics.Vector3 Translation { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Vector3Transition TranslationTransition { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool UseLayoutRounding { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool UseSystemFocusVisuals { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Visibility Visibility { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.DependencyObject XYFocusDown { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Input.XYFocusNavigationStrategy XYFocusDownNavigationStrategy { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Input.XYFocusKeyboardNavigationMode XYFocusKeyboardNavigation { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.DependencyObject XYFocusLeft { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Input.XYFocusNavigationStrategy XYFocusLeftNavigationStrategy { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.DependencyObject XYFocusRight { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Input.XYFocusNavigationStrategy XYFocusRightNavigationStrategy { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.DependencyObject XYFocusUp { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Input.XYFocusNavigationStrategy XYFocusUpNavigationStrategy { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.XamlRoot XamlRoot { get; set; } = default!;
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Controls.ContextMenuEventArgs> OnContextMenuOpening { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Controls.IsTextTrimmedChangedEventArgs> OnIsTextTrimmedChanged { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs> OnSelectionChanged { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<object> OnActualThemeChanged { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DataContextChangedEventArgs> OnDataContextChanged { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.EffectiveViewportChangedEventArgs> OnEffectiveViewportChanged { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<object> OnLayoutUpdated { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs> OnLoaded { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<object> OnLoading { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.SizeChangedEventArgs> OnSizeChanged { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs> OnUnloaded { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.AccessKeyDisplayDismissedEventArgs> OnAccessKeyDisplayDismissed { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.AccessKeyDisplayRequestedEventArgs> OnAccessKeyDisplayRequested { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.AccessKeyInvokedEventArgs> OnAccessKeyInvoked { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.BringIntoViewRequestedEventArgs> OnBringIntoViewRequested { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.CharacterReceivedRoutedEventArgs> OnCharacterReceived { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs> OnContextCanceled { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ContextRequestedEventArgs> OnContextRequested { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs> OnDoubleTapped { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DragEventArgs> OnDragEnter { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DragEventArgs> OnDragLeave { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DragEventArgs> OnDragOver { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DragStartingEventArgs> OnDragStarting { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DragEventArgs> OnDrop { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.DropCompletedEventArgs> OnDropCompleted { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.GettingFocusEventArgs> OnGettingFocus { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs> OnGotFocus { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.HoldingRoutedEventArgs> OnHolding { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs> OnKeyDown { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs> OnKeyUp { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.LosingFocusEventArgs> OnLosingFocus { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs> OnLostFocus { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ManipulationCompletedRoutedEventArgs> OnManipulationCompleted { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ManipulationDeltaRoutedEventArgs> OnManipulationDelta { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ManipulationInertiaStartingRoutedEventArgs> OnManipulationInertiaStarting { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ManipulationStartedRoutedEventArgs> OnManipulationStarted { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ManipulationStartingRoutedEventArgs> OnManipulationStarting { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.NoFocusCandidateFoundEventArgs> OnNoFocusCandidateFound { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> OnPointerCanceled { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> OnPointerCaptureLost { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> OnPointerEntered { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> OnPointerExited { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> OnPointerMoved { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> OnPointerPressed { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> OnPointerReleased { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs> OnPointerWheelChanged { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs> OnPreviewKeyDown { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.KeyRoutedEventArgs> OnPreviewKeyUp { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.ProcessKeyboardAcceleratorEventArgs> OnProcessKeyboardAccelerators { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.RightTappedRoutedEventArgs> OnRightTapped { get; set; }
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.Input.TappedRoutedEventArgs> OnTapped { get; set; }
}
