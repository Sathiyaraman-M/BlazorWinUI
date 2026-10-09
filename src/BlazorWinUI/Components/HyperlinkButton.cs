#nullable enable
namespace BlazorWinUI.Components;

/// <summary>
/// Displays a button styled as a hyperlink and can navigate to a URI.
/// </summary>
public sealed class HyperlinkButton : global::Microsoft.AspNetCore.Components.ComponentBase
{
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public object? Content { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::System.Uri? NavigateUri { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public bool IsEnabled { get; set; } = true;

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::System.Windows.Input.ICommand? Command { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public object? CommandParameter { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public string? AutomationName { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public object? ToolTip { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs> OnClick { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.Brush Background { get; set; } = default!;

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.Brush Foreground { get; set; } = default!;

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Media.Brush BorderBrush { get; set; } = default!;

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Thickness BorderThickness { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.CornerRadius CornerRadius { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.Thickness Padding { get; set; }

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
    public global::Microsoft.UI.Xaml.HorizontalAlignment HorizontalContentAlignment { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.UI.Xaml.VerticalAlignment VerticalContentAlignment { get; set; }

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

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
    {
        builder.AddContent(0, ChildContent);
    }
}
