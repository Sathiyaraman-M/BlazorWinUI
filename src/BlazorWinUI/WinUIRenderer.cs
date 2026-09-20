using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Dispatching;

namespace BlazorWinUI;

internal sealed class WinUIRenderer(IServiceProvider serviceProvider, DispatcherQueue dispatcherQueue, ILoggerFactory loggerFactory) : Renderer(serviceProvider, loggerFactory)
{
    private readonly WinUIDispatcher _dispatcher = new(dispatcherQueue);

    public override Dispatcher Dispatcher => _dispatcher;

    protected override void HandleException(Exception exception)
    {
        throw new NotImplementedException();
    }

    protected override Task UpdateDisplayAsync(in RenderBatch renderBatch)
    {
        throw new NotImplementedException();
    }
}
