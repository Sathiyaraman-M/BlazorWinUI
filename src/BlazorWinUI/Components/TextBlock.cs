using Microsoft.AspNetCore.Components;

namespace BlazorWinUI.Components;

public sealed class TextBlock : ComponentBase
{
    [Parameter]
    public string? Text { get; set; }
}
