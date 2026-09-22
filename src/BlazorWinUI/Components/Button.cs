using Microsoft.AspNetCore.Components;

namespace BlazorWinUI.Components;

public sealed class Button : ComponentBase
{
    [Parameter]
    public string? Text { get; set; }

    [Parameter]
    public EventCallback OnClick { get; set; }
}
