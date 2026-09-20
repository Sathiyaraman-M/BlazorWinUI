using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Dispatching;

namespace BlazorWinUI;

internal sealed class WinUIRenderer(IServiceProvider serviceProvider, DispatcherQueue dispatcherQueue, ILoggerFactory loggerFactory) : Renderer(serviceProvider, loggerFactory)
{
    private readonly WinUIDispatcher _dispatcher = new(dispatcherQueue);
    private readonly ILogger<WinUIRenderer> _logger = loggerFactory.CreateLogger<WinUIRenderer>();

    public override Dispatcher Dispatcher => _dispatcher;

    public event EventHandler<UnhandledExceptionEventArgs>? OnUnhandledException;

    protected override void HandleException(Exception exception)
    {
        _logger.LogError(exception, "Unhandled Exception in the WinUI Renderer");
        OnUnhandledException?.Invoke(this, new UnhandledExceptionEventArgs(exception, false));
    }

    protected override Task UpdateDisplayAsync(in RenderBatch renderBatch)
    {
        throw new NotImplementedException();
    }
}
