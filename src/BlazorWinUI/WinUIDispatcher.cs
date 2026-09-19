using Microsoft.AspNetCore.Components;

namespace BlazorWinUI;

internal sealed class WinUIDispatcher : Dispatcher
{
    public override bool CheckAccess()
    {
        throw new NotImplementedException();
    }

    public override Task InvokeAsync(Action workItem)
    {
        throw new NotImplementedException();
    }

    public override Task InvokeAsync(Func<Task> workItem)
    {
        throw new NotImplementedException();
    }

    public override Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
    {
        throw new NotImplementedException();
    }

    public override Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
    {
        throw new NotImplementedException();
    }
}