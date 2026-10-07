using BlazorWinUI.Abstractions;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWinUI;

internal sealed class AdapterResolver(IServiceProvider services)
{
    private readonly Dictionary<Type, Type> _registrations = [];

    public void Register<TComponent, TAdapter>()
        where TComponent : IComponent
        where TAdapter : class, IAdapter
    {
        _registrations[typeof(TComponent)] = typeof(TAdapter);
    }

    public static AdapterResolver CreateDefault(IServiceProvider services)
    {
        var resolver = new AdapterResolver(services);
        DefaultAdapterRegistry.RegisterDefaults(resolver);
        return resolver;
    }

    public bool HasAdapter(Type componentType)
    {
        ArgumentNullException.ThrowIfNull(componentType);
        return _registrations.ContainsKey(componentType);
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
