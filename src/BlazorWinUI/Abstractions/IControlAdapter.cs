using Microsoft.AspNetCore.Components;
using Microsoft.UI.Xaml;

namespace BlazorWinUI.Abstractions;

internal interface IControlAdapter : IDisposable
{
    public UIElement Element { get; }

    public void ApplyParameters(ParameterView parameterView);
}
