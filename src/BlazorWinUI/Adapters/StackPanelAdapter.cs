using BlazorWinUI.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BlazorWinUI.Adapters;

public sealed class StackPanelAdapter : IAdapter, IControlContainer
{
    public FrameworkElement Element { get; } = new StackPanel();

    public void ApplyParameters(ParameterView parameterView)
    {
    }

    public void SetChildren(IReadOnlyList<FrameworkElement> children)
    {
        var panel = (StackPanel)Element;
        panel.Children.Clear();

        foreach (var child in children)
        {
            panel.Children.Add(child);
        }
    }

    public void Dispose()
    {
    }
}
