#nullable enable
namespace BlazorWinUI.Adapters;

public sealed class HyperlinkButtonAdapter : global::BlazorWinUI.Abstractions.IAdapter, global::BlazorWinUI.Abstractions.IControlContainer
{
    private readonly global::Microsoft.UI.Xaml.Controls.HyperlinkButton _control = new();
    private readonly object? _defaultContent;
    private readonly global::System.Uri? _defaultNavigateUri;
    private readonly bool _defaultIsEnabled;
    private readonly global::System.Windows.Input.ICommand _defaultCommand;
    private readonly object _defaultCommandParameter;
    private readonly string _defaultAutomationName;
    private readonly object _defaultToolTip;
    private readonly global::Microsoft.UI.Xaml.Media.Brush _defaultBackground;
    private readonly global::Microsoft.UI.Xaml.Media.Brush _defaultForeground;
    private readonly global::Microsoft.UI.Xaml.Media.Brush _defaultBorderBrush;
    private readonly global::Microsoft.UI.Xaml.Thickness _defaultBorderThickness;
    private readonly global::Microsoft.UI.Xaml.CornerRadius _defaultCornerRadius;
    private readonly global::Microsoft.UI.Xaml.Thickness _defaultPadding;
    private readonly global::Microsoft.UI.Xaml.Media.FontFamily _defaultFontFamily;
    private readonly double _defaultFontSize;
    private readonly global::Windows.UI.Text.FontStyle _defaultFontStyle;
    private readonly global::Windows.UI.Text.FontWeight _defaultFontWeight;
    private readonly bool _defaultIsTextScaleFactorEnabled;
    private readonly global::Microsoft.UI.Xaml.HorizontalAlignment _defaultHorizontalContentAlignment;
    private readonly global::Microsoft.UI.Xaml.VerticalAlignment _defaultVerticalContentAlignment;
    private readonly global::Microsoft.UI.Xaml.HorizontalAlignment _defaultHorizontalAlignment;
    private readonly global::Microsoft.UI.Xaml.VerticalAlignment _defaultVerticalAlignment;
    private readonly global::Microsoft.UI.Xaml.Thickness _defaultMargin;
    private readonly double _defaultHeight;
    private readonly double _defaultWidth;
    private readonly global::Microsoft.UI.Xaml.Visibility _defaultVisibility;
    private readonly string _defaultName;
    private readonly global::Microsoft.UI.Xaml.Style _defaultStyle;
    private readonly global::Microsoft.UI.Xaml.ElementTheme _defaultRequestedTheme;
    private readonly global::Microsoft.UI.Xaml.FlowDirection _defaultFlowDirection;
    private readonly object _defaultDataContext;
    private global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs> _onClick;
    private object? _configuredContent;
    private bool _hasRenderedChildren;

    public global::Microsoft.UI.Xaml.FrameworkElement Element => _control;

    public HyperlinkButtonAdapter()
    {
        _defaultContent = _control.Content;
        _configuredContent = _defaultContent;
        _defaultNavigateUri = _control.NavigateUri;
        _defaultIsEnabled = _control.IsEnabled;
        _defaultCommand = _control.Command;
        _defaultCommandParameter = _control.CommandParameter;
        _defaultAutomationName = global::Microsoft.UI.Xaml.Automation.AutomationProperties.GetName(_control);
        _defaultToolTip = global::Microsoft.UI.Xaml.Controls.ToolTipService.GetToolTip(_control);
        _defaultBackground = _control.Background;
        _defaultForeground = _control.Foreground;
        _defaultBorderBrush = _control.BorderBrush;
        _defaultBorderThickness = _control.BorderThickness;
        _defaultCornerRadius = _control.CornerRadius;
        _defaultPadding = _control.Padding;
        _defaultFontFamily = _control.FontFamily;
        _defaultFontSize = _control.FontSize;
        _defaultFontStyle = _control.FontStyle;
        _defaultFontWeight = _control.FontWeight;
        _defaultIsTextScaleFactorEnabled = _control.IsTextScaleFactorEnabled;
        _defaultHorizontalContentAlignment = _control.HorizontalContentAlignment;
        _defaultVerticalContentAlignment = _control.VerticalContentAlignment;
        _defaultHorizontalAlignment = _control.HorizontalAlignment;
        _defaultVerticalAlignment = _control.VerticalAlignment;
        _defaultMargin = _control.Margin;
        _defaultHeight = _control.Height;
        _defaultWidth = _control.Width;
        _defaultVisibility = _control.Visibility;
        _defaultName = _control.Name;
        _defaultStyle = _control.Style;
        _defaultRequestedTheme = _control.RequestedTheme;
        _defaultFlowDirection = _control.FlowDirection;
        _defaultDataContext = _control.DataContext;
        _control.Click += OnOnClick;
    }

    public void ApplyParameters(global::Microsoft.AspNetCore.Components.ParameterView parameterView)
    {
        var currentParameters = new global::System.Collections.Generic.HashSet<string>(global::System.StringComparer.Ordinal);
        foreach (var parameter in parameterView)
        {
            currentParameters.Add(parameter.Name);
            switch (parameter.Name)
            {
                case "Content":
                    _configuredContent = parameter.Value;
                    if (!_hasRenderedChildren) _control.Content = _configuredContent;
                    break;
                case "NavigateUri": _control.NavigateUri = (global::System.Uri?)parameter.Value; break;
                case "IsEnabled": _control.IsEnabled = (bool)parameter.Value!; break;
                case "Command": _control.Command = (global::System.Windows.Input.ICommand)parameter.Value!; break;
                case "CommandParameter": _control.CommandParameter = parameter.Value!; break;
                case "AutomationName": global::Microsoft.UI.Xaml.Automation.AutomationProperties.SetName(_control, (string?)parameter.Value); break;
                case "ToolTip": global::Microsoft.UI.Xaml.Controls.ToolTipService.SetToolTip(_control, parameter.Value); break;
                case "OnClick": _onClick = (global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs>)parameter.Value!; break;
                case "Background": _control.Background = (global::Microsoft.UI.Xaml.Media.Brush)parameter.Value!; break;
                case "Foreground": _control.Foreground = (global::Microsoft.UI.Xaml.Media.Brush)parameter.Value!; break;
                case "BorderBrush": _control.BorderBrush = (global::Microsoft.UI.Xaml.Media.Brush)parameter.Value!; break;
                case "BorderThickness": _control.BorderThickness = (global::Microsoft.UI.Xaml.Thickness)parameter.Value!; break;
                case "CornerRadius": _control.CornerRadius = (global::Microsoft.UI.Xaml.CornerRadius)parameter.Value!; break;
                case "Padding": _control.Padding = (global::Microsoft.UI.Xaml.Thickness)parameter.Value!; break;
                case "FontFamily": _control.FontFamily = (global::Microsoft.UI.Xaml.Media.FontFamily)parameter.Value!; break;
                case "FontSize": _control.FontSize = (double)parameter.Value!; break;
                case "FontStyle": _control.FontStyle = (global::Windows.UI.Text.FontStyle)parameter.Value!; break;
                case "FontWeight": _control.FontWeight = (global::Windows.UI.Text.FontWeight)parameter.Value!; break;
                case "IsTextScaleFactorEnabled": _control.IsTextScaleFactorEnabled = (bool)parameter.Value!; break;
                case "HorizontalContentAlignment": _control.HorizontalContentAlignment = (global::Microsoft.UI.Xaml.HorizontalAlignment)parameter.Value!; break;
                case "VerticalContentAlignment": _control.VerticalContentAlignment = (global::Microsoft.UI.Xaml.VerticalAlignment)parameter.Value!; break;
                case "HorizontalAlignment": _control.HorizontalAlignment = (global::Microsoft.UI.Xaml.HorizontalAlignment)parameter.Value!; break;
                case "VerticalAlignment": _control.VerticalAlignment = (global::Microsoft.UI.Xaml.VerticalAlignment)parameter.Value!; break;
                case "Margin": _control.Margin = (global::Microsoft.UI.Xaml.Thickness)parameter.Value!; break;
                case "Height": _control.Height = (double)parameter.Value!; break;
                case "Width": _control.Width = (double)parameter.Value!; break;
                case "Visibility": _control.Visibility = (global::Microsoft.UI.Xaml.Visibility)parameter.Value!; break;
                case "Name": _control.Name = (string)parameter.Value!; break;
                case "Style": _control.Style = (global::Microsoft.UI.Xaml.Style)parameter.Value!; break;
                case "RequestedTheme": _control.RequestedTheme = (global::Microsoft.UI.Xaml.ElementTheme)parameter.Value!; break;
                case "FlowDirection": _control.FlowDirection = (global::Microsoft.UI.Xaml.FlowDirection)parameter.Value!; break;
                case "DataContext": _control.DataContext = (object)parameter.Value!; break;
            }
        }

        if (!currentParameters.Contains("Content")) _configuredContent = _defaultContent;
        if (!_hasRenderedChildren) _control.Content = _configuredContent;
        if (!currentParameters.Contains("NavigateUri")) _control.NavigateUri = _defaultNavigateUri;
        if (!currentParameters.Contains("IsEnabled")) _control.IsEnabled = _defaultIsEnabled;
        if (!currentParameters.Contains("Command")) _control.Command = _defaultCommand;
        if (!currentParameters.Contains("CommandParameter")) _control.CommandParameter = _defaultCommandParameter;
        if (!currentParameters.Contains("AutomationName")) global::Microsoft.UI.Xaml.Automation.AutomationProperties.SetName(_control, _defaultAutomationName);
        if (!currentParameters.Contains("ToolTip")) global::Microsoft.UI.Xaml.Controls.ToolTipService.SetToolTip(_control, _defaultToolTip);
        if (!currentParameters.Contains("OnClick")) _onClick = default;
        if (!currentParameters.Contains("Background")) _control.Background = _defaultBackground;
        if (!currentParameters.Contains("Foreground")) _control.Foreground = _defaultForeground;
        if (!currentParameters.Contains("BorderBrush")) _control.BorderBrush = _defaultBorderBrush;
        if (!currentParameters.Contains("BorderThickness")) _control.BorderThickness = _defaultBorderThickness;
        if (!currentParameters.Contains("CornerRadius")) _control.CornerRadius = _defaultCornerRadius;
        if (!currentParameters.Contains("Padding")) _control.Padding = _defaultPadding;
        if (!currentParameters.Contains("FontFamily")) _control.FontFamily = _defaultFontFamily;
        if (!currentParameters.Contains("FontSize")) _control.FontSize = _defaultFontSize;
        if (!currentParameters.Contains("FontStyle")) _control.FontStyle = _defaultFontStyle;
        if (!currentParameters.Contains("FontWeight")) _control.FontWeight = _defaultFontWeight;
        if (!currentParameters.Contains("IsTextScaleFactorEnabled")) _control.IsTextScaleFactorEnabled = _defaultIsTextScaleFactorEnabled;
        if (!currentParameters.Contains("HorizontalContentAlignment")) _control.HorizontalContentAlignment = _defaultHorizontalContentAlignment;
        if (!currentParameters.Contains("VerticalContentAlignment")) _control.VerticalContentAlignment = _defaultVerticalContentAlignment;
        if (!currentParameters.Contains("HorizontalAlignment")) _control.HorizontalAlignment = _defaultHorizontalAlignment;
        if (!currentParameters.Contains("VerticalAlignment")) _control.VerticalAlignment = _defaultVerticalAlignment;
        if (!currentParameters.Contains("Margin")) _control.Margin = _defaultMargin;
        if (!currentParameters.Contains("Height")) _control.Height = _defaultHeight;
        if (!currentParameters.Contains("Width")) _control.Width = _defaultWidth;
        if (!currentParameters.Contains("Visibility")) _control.Visibility = _defaultVisibility;
        if (!currentParameters.Contains("Name")) _control.Name = _defaultName;
        if (!currentParameters.Contains("Style")) _control.Style = _defaultStyle;
        if (!currentParameters.Contains("RequestedTheme")) _control.RequestedTheme = _defaultRequestedTheme;
        if (!currentParameters.Contains("FlowDirection")) _control.FlowDirection = _defaultFlowDirection;
        if (!currentParameters.Contains("DataContext")) _control.DataContext = _defaultDataContext;
    }

    private async void OnOnClick(object sender, global::Microsoft.UI.Xaml.RoutedEventArgs args)
    {
        await _onClick.InvokeAsync(args);
    }

    public void SetChildren(global::System.Collections.Generic.IReadOnlyList<global::Microsoft.UI.Xaml.FrameworkElement> children)
    {
        if (children.Count > 1)
        {
            throw new global::System.InvalidOperationException("This WinUI control accepts at most one child component.");
        }

        _hasRenderedChildren = children.Count > 0;
        _control.Content = _hasRenderedChildren ? children[0] : _configuredContent;
    }

    public void Dispose()
    {
        _control.Click -= OnOnClick;
    }
}
