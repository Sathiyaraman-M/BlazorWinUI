using BlazorWinUI.Abstractions;

using Microsoft.AspNetCore.Components;
using Microsoft.UI.Xaml;

using WinUIButton = Microsoft.UI.Xaml.Controls.Button;

namespace BlazorWinUI.Adapters;

public sealed class ButtonAdapter : IAdapter
{
    private readonly WinUIButton _button = new();
    private EventCallback _onClick;

    public ButtonAdapter()
    {
        _button.Click += OnClick;
    }

    public FrameworkElement Element => _button;

    public void ApplyParameters(ParameterView parameterView)
    {
        foreach (var parameter in parameterView)
        {
            switch (parameter.Name)
            {
                case "Text" when parameter.Value is string text:
                    _button.Content = text;
                    break;
                case "OnClick" when parameter.Value is EventCallback callback:
                    _onClick = callback;
                    break;
            }
        }
    }

    private async void OnClick(object sender, RoutedEventArgs args)
    {
        await _onClick.InvokeAsync();
    }

    public void Dispose()
    {
        _button.Click -= OnClick;
    }
}
