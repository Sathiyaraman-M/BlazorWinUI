namespace BlazorWinUI.Abstractions;

internal sealed class NativeControl(int componentId, IControlAdapter? adapter, IControlContainer? container = null)
{
    public int ComponentId { get; } = componentId;

    public IControlAdapter? Adapter { get; } = adapter;

    public IControlContainer? Container { get; } = container;

    public NativeControl? Parent { get; set; }

    public List<NativeControl> Children { get; } = [];
}
