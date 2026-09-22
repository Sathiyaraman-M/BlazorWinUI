using Microsoft.AspNetCore.Components;
using Microsoft.UI.Xaml;

namespace BlazorWinUI.Abstractions;

public interface IAdapter : IDisposable
{
    public FrameworkElement Element { get; }

    public void ApplyParameters(ParameterView parameterView);
}
