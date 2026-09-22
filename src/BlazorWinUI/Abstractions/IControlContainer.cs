namespace BlazorWinUI.Abstractions;

public interface IControlContainer
{
    public void SetChildren(IReadOnlyList<Microsoft.UI.Xaml.FrameworkElement> children);
}
