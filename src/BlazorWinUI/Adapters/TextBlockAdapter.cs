using BlazorWinUI.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BlazorWinUI.Adapters;

public sealed class TextBlockAdapter : IAdapter
{
    public FrameworkElement Element { get; } = new TextBlock();

    public void ApplyParameters(ParameterView parameterView)
    {
        var textBlock = (TextBlock)Element;

        foreach (var parameter in parameterView)
        {
            if (string.Equals(parameter.Name, "Text", StringComparison.Ordinal) &&
                parameter.Value is string text)
            {
                textBlock.Text = text;
            }
        }
    }

    public void Dispose()
    {
    }
}
