namespace BlazorWinUI.Abstractions;

internal interface IControlContainer
{
    public void SetChildren(IReadOnlyList<IAdapter> children);
}
