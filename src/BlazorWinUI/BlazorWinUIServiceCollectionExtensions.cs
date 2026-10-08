using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.UI.Dispatching;

namespace BlazorWinUI;

/// <summary>Registers the shared renderer used by XAML-hosted Blazor components.</summary>
public static class BlazorWinUIServiceCollectionExtensions
{
    /// <summary>
    /// Registers one <see cref="WinUIRenderer"/> for the application service provider and UI dispatcher.
    /// </summary>
    public static IServiceCollection AddBlazorWinUI(
        this IServiceCollection services,
        DispatcherQueue dispatcherQueue)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(dispatcherQueue);

        if (!dispatcherQueue.HasThreadAccess)
        {
            throw new InvalidOperationException(
                "AddBlazorWinUI must be called on the UI thread associated with the supplied DispatcherQueue.");
        }

        services.TryAddSingleton(serviceProvider =>
        {
            if (!dispatcherQueue.HasThreadAccess)
            {
                throw new InvalidOperationException(
                    "The BlazorWinUI renderer must be resolved on the UI thread associated with its DispatcherQueue.");
            }

            var loggerFactory = serviceProvider.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;

            return new WinUIRenderer(serviceProvider, dispatcherQueue, loggerFactory);
        });

        return services;
    }
}
