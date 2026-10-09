#nullable enable
namespace BlazorWinUI.Adapters;

public sealed class FontIconAdapter : global::BlazorWinUI.Abstractions.IAdapter
{
    private readonly global::Microsoft.UI.Xaml.Controls.FontIcon _control = new();
    private readonly string _defaultGlyph;
    private readonly global::Microsoft.UI.Xaml.Media.FontFamily _defaultFontFamily;
    private readonly double _defaultFontSize;
    private readonly global::Windows.UI.Text.FontStyle _defaultFontStyle;
    private readonly global::Windows.UI.Text.FontWeight _defaultFontWeight;
    private readonly bool _defaultIsTextScaleFactorEnabled;
    private readonly bool _defaultMirroredWhenRightToLeft;
    private readonly global::Microsoft.UI.Xaml.Media.Brush _defaultForeground;
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

    public global::Microsoft.UI.Xaml.FrameworkElement Element => _control;

    public FontIconAdapter()
    {
        _defaultGlyph = _control.Glyph;
        _defaultFontFamily = _control.FontFamily;
        _defaultFontSize = _control.FontSize;
        _defaultFontStyle = _control.FontStyle;
        _defaultFontWeight = _control.FontWeight;
        _defaultIsTextScaleFactorEnabled = _control.IsTextScaleFactorEnabled;
        _defaultMirroredWhenRightToLeft = _control.MirroredWhenRightToLeft;
        _defaultForeground = _control.Foreground;
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
    }

    public void ApplyParameters(global::Microsoft.AspNetCore.Components.ParameterView parameterView)
    {
        var currentParameters = new global::System.Collections.Generic.HashSet<string>(global::System.StringComparer.Ordinal);
        foreach (var parameter in parameterView)
        {
            currentParameters.Add(parameter.Name);
            switch (parameter.Name)
            {
                case "Glyph": _control.Glyph = (string)parameter.Value!; break;
                case "FontFamily": _control.FontFamily = (global::Microsoft.UI.Xaml.Media.FontFamily)parameter.Value!; break;
                case "FontSize": _control.FontSize = (double)parameter.Value!; break;
                case "FontStyle": _control.FontStyle = (global::Windows.UI.Text.FontStyle)parameter.Value!; break;
                case "FontWeight": _control.FontWeight = (global::Windows.UI.Text.FontWeight)parameter.Value!; break;
                case "IsTextScaleFactorEnabled": _control.IsTextScaleFactorEnabled = (bool)parameter.Value!; break;
                case "MirroredWhenRightToLeft": _control.MirroredWhenRightToLeft = (bool)parameter.Value!; break;
                case "Foreground": _control.Foreground = (global::Microsoft.UI.Xaml.Media.Brush)parameter.Value!; break;
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

        if (!currentParameters.Contains("Glyph")) _control.Glyph = _defaultGlyph;
        if (!currentParameters.Contains("FontFamily")) _control.FontFamily = _defaultFontFamily;
        if (!currentParameters.Contains("FontSize")) _control.FontSize = _defaultFontSize;
        if (!currentParameters.Contains("FontStyle")) _control.FontStyle = _defaultFontStyle;
        if (!currentParameters.Contains("FontWeight")) _control.FontWeight = _defaultFontWeight;
        if (!currentParameters.Contains("IsTextScaleFactorEnabled")) _control.IsTextScaleFactorEnabled = _defaultIsTextScaleFactorEnabled;
        if (!currentParameters.Contains("MirroredWhenRightToLeft")) _control.MirroredWhenRightToLeft = _defaultMirroredWhenRightToLeft;
        if (!currentParameters.Contains("Foreground")) _control.Foreground = _defaultForeground;
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

    public void Dispose()
    {
    }
}
