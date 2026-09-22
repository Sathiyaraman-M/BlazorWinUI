using BlazorWinUI.Abstractions;

using Microsoft.Extensions.DependencyInjection;

namespace BlazorWinUI;

internal sealed class ControlAdapterResolver(IServiceProvider services)
{
    private readonly Dictionary<Type, Type> _registrations = [];

    public void Register<TComponent, TAdapter>()
        where TAdapter : class, IControlAdapter
    {
        _registrations[typeof(TComponent)] = typeof(TAdapter);
    }

    public static ControlAdapterResolver CreateDefault(IServiceProvider services)
    {
        return new ControlAdapterResolver(services);
    }

    public IControlAdapter Create(Type componentType)
    {
        ArgumentNullException.ThrowIfNull(componentType);

        if (!_registrations.TryGetValue(componentType, out var adapterType))
        {
            throw new NotSupportedException(
                $"No native WinUI control adapter is registered for component '{componentType.FullName}'.");
        }

        return (IControlAdapter)ActivatorUtilities.CreateInstance(services, adapterType);
    }
}