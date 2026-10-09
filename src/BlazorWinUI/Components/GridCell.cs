#nullable enable
namespace BlazorWinUI.Components;

/// <summary>
/// Applies WinUI Grid placement to each native control produced by this component's child content.
/// </summary>
/// <remarks>
/// This is a renderer scope rather than a native visual element. It may contain zero or more
/// native controls; all controls it produces receive the same row, column, and span values.
/// </remarks>
public sealed class GridCell : global::Microsoft.AspNetCore.Components.ComponentBase
{
    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public int Row { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public int Column { get; set; }

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public int RowSpan { get; set; } = 1;

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public int ColumnSpan { get; set; } = 1;

    [global::Microsoft.AspNetCore.Components.ParameterAttribute]
    public global::Microsoft.AspNetCore.Components.RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
    {
        builder.AddContent(0, ChildContent);
    }
}
