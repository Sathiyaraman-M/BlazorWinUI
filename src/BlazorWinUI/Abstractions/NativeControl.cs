namespace BlazorWinUI.Abstractions;

internal sealed class NativeControl(int componentId, IControlAdapter? adapter)
{
    public int ComponentId { get; } = componentId;

    public IControlAdapter? Adapter { get; } = adapter;

    public NativeControl? Parent { get; set; }

    public List<NativeControl> Children { get; } = [];
}
