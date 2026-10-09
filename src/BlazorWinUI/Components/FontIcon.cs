#nullable enable
namespace BlazorWinUI.Components;

/// <summary>
/// Displays a glyph from a font as a WinUI element.
/// </summary>
public sealed class FontIcon : global::Microsoft.AspNetCore.Components.ComponentBase
{
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public string Glyph { get; set; } = string.Empty;

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.FontFamily FontFamily { get; set; } = default!;

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public double FontSize { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Windows.UI.Text.FontStyle FontStyle { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Windows.UI.Text.FontWeight FontWeight { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool IsTextScaleFactorEnabled { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool MirroredWhenRightToLeft { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.Brush Foreground { get; set; } = default!;

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.HorizontalAlignment HorizontalAlignment { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.VerticalAlignment VerticalAlignment { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Thickness Margin { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public double Height { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public double Width { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Visibility Visibility { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public string Name { get; set; } = string.Empty;

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Style Style { get; set; } = default!;

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.ElementTheme RequestedTheme { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.FlowDirection FlowDirection { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public object DataContext { get; set; } = default!;
}
