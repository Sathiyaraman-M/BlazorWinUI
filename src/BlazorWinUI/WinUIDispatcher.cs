using Microsoft.AspNetCore.Components;
using Microsoft.UI.Dispatching;

namespace BlazorWinUI;

internal sealed class WinUIDispatcher(DispatcherQueue queue) : Dispatcher
{
    public override bool CheckAccess()
    {
        return queue.HasThreadAccess;
    }

    public override Task InvokeAsync(Action workItem)
    {
        if (CheckAccess())
        {
            try
            {
                workItem();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        var taskCompletionSource = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

        if (!queue.TryEnqueue(() =>
        {
            try
            {
                workItem();
                taskCompletionSource.SetResult();
            }
            catch (Exception ex)
            {
                taskCompletionSource.SetException(ex);
            }
        }))
        {
            taskCompletionSource.TrySetException(new InvalidOperationException(
                "The WinUI DispatcherQueue is no longer accepting work."));
        }

        return taskCompletionSource.Task;
    }

    public override Task InvokeAsync(Func<Task> workItem)
    {
        if (CheckAccess())
        {
            return workItem();
        }

        var taskCompletionSource = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

        if (!queue.TryEnqueue(async () =>
        {
            try
            {
                await workItem().ConfigureAwait(true);
                taskCompletionSource.SetResult();
            }
            catch (Exception ex)
            {
                taskCompletionSource.SetException(ex);
            }
        }))
        {
            taskCompletionSource.TrySetException(new InvalidOperationException(
                "The WinUI DispatcherQueue is no longer accepting work."));
        }

        return taskCompletionSource.Task;
    }

    public override Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
    {
        if (CheckAccess())
        {
            try
            {
                var result = workItem();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                return Task.FromException<TResult>(ex);
            }
        }

        var taskCompletionSource = new TaskCompletionSource<TResult>(TaskCreationOptions.RunContinuationsAsynchronously);

        if (!queue.TryEnqueue(() =>
        {
            try
            {
                var result = workItem();
                taskCompletionSource.SetResult(result);
            }
            catch (Exception ex)
            {
                taskCompletionSource.SetException(ex);
            }
        }))
        {
            taskCompletionSource.TrySetException(new InvalidOperationException(
                "The WinUI DispatcherQueue is no longer accepting work."));
        }

        return taskCompletionSource.Task;
    }

    public override Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
    {
        if (CheckAccess())
        {
            return workItem();
        }

        var taskCompletionSource = new TaskCompletionSource<TResult>(TaskCreationOptions.RunContinuationsAsynchronously);

        if (!queue.TryEnqueue(async () =>
        {
            try
            {
                var result = await workItem().ConfigureAwait(true);
                taskCompletionSource.SetResult(result);
            }
            catch (Exception ex)
            {
                taskCompletionSource.SetException(ex);
            }
        }))
        {
            taskCompletionSource.TrySetException(new InvalidOperationException(
                "The WinUI DispatcherQueue is no longer accepting work."));
        }

        return taskCompletionSource.Task;
    }
}
