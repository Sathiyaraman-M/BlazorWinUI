using BlazorWinUI.Abstractions;

using Microsoft.Extensions.DependencyInjection;

namespace BlazorWinUI;

internal sealed class AdapterResolver(IServiceProvider services)
{
    private readonly Dictionary<Type, Type> _registrations = [];

    public void Register<TComponent, TAdapter>()
        where TAdapter : class, IAdapter
    {
        _registrations[typeof(TComponent)] = typeof(TAdapter);
    }

    public static AdapterResolver CreateDefault(IServiceProvider services)
    {
        return new AdapterResolver(services);
    }

    public IAdapter Create(Type componentType)
    {
        ArgumentNullException.ThrowIfNull(componentType);

        if (!_registrations.TryGetValue(componentType, out var adapterType))
        {
            throw new NotSupportedException(
                $"No native WinUI control adapter is registered for component '{componentType.FullName}'.");
        }

        return (IAdapter)ActivatorUtilities.CreateInstance(services, adapterType);
    }
}
