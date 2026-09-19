using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Logging;

namespace BlazorWinUI;

public class WinUIRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory) : Renderer(serviceProvider, loggerFactory)
{
    private readonly WinUIDispatcher _dispatcher = new();

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
