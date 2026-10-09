namespace BlazorWinUI.Abstractions;

internal sealed class NativeControl(int componentId, IAdapter? adapter, IControlContainer? container = null)
{
    public int ComponentId { get; } = componentId;

    public IAdapter? Adapter { get; } = adapter;

    public IControlContainer? Container { get; } = container;

    public NativeControl? Parent { get; set; }

    public GridCellPlacement? GridCellPlacement { get; set; }

    public List<NativeControl> Children { get; } = [];
}

internal readonly record struct GridCellPlacement(int Row, int Column, int RowSpan, int ColumnSpan);
