using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;

using BlazorWinUI.Abstractions;
using BlazorWinUI.Adapters;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Xunit;

namespace BlazorWinUI.Tests;

public sealed class AdapterBehaviorTests
{
    [Fact]
    public void SharedRendererAndXamlHostsSupportIncrementalMounting()
    {
        RunOnXamlThreadAsync(async () =>
        {
            var invalidHost = new BlazorComponentHost();
            Assert.Throws<ArgumentException>(() => invalidHost.ComponentType = typeof(string));

            var dispatcherQueue = DispatcherQueue.GetForCurrentThread()
                ?? throw new InvalidOperationException("The test thread has no WinUI DispatcherQueue.");
            var services = new ServiceCollection()
                .AddBlazorWinUI(dispatcherQueue)
                .BuildServiceProvider();
            var renderer = services.GetRequiredService<WinUIRenderer>();
            Assert.Same(renderer, services.GetRequiredService<WinUIRenderer>());

            Window? window = null;
            try
            {
                var firstHost = new Grid();
                var secondHost = new Grid();
                var firstParameters = ParameterView.FromDictionary(new Dictionary<string, object?> { ["Text"] = "First" });
                var secondParameters = ParameterView.FromDictionary(new Dictionary<string, object?> { ["Text"] = "Second" });
                var disposedComponentsBeforeUnmount = TextRootComponent.DisposedCount;

                var firstId = await renderer.MountRootComponentAsync(typeof(TextRootComponent), firstHost, firstParameters);
                await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    await renderer.MountRootComponentAsync(typeof(TextRootComponent), firstHost, secondParameters));
                await renderer.UpdateRootComponentAsync(firstId, secondParameters);
                var secondId = await renderer.MountRootComponentAsync<TextRootComponent>(secondHost);

                Assert.NotEqual(firstId, secondId);
                Assert.Equal("Second", Assert.IsType<TextBlock>(Assert.Single(firstHost.Children)).Text);
                Assert.Single(secondHost.Children);

                await renderer.UnmountRootComponentAsync(firstId);
                await renderer.UnmountRootComponentAsync(firstId);
                await renderer.UnmountRootComponentAsync(secondId);
                Assert.Empty(firstHost.Children);
                Assert.Empty(secondHost.Children);
                Assert.Equal(disposedComponentsBeforeUnmount + 2, TextRootComponent.DisposedCount);
                Assert.Equal(0, GetNativeControlCount(renderer));

                var host = new BlazorComponentHost
                {
                    Renderer = renderer,
                    ComponentType = typeof(TextRootComponent),
                    Parameters = new Dictionary<string, object?> { ["Text"] = "Initial" }
                };
                var secondXamlHost = new BlazorComponentHost
                {
                    Renderer = renderer,
                    ComponentType = typeof(TextRootComponent),
                    Parameters = new Dictionary<string, object?> { ["Text"] = "Shared renderer" }
                };
                var rootPanel = Assert.IsType<Grid>(host.Content);
                Exception? hostException = null;
                var hostErrorCount = 0;
                var rendererExceptionCount = 0;
                var loadedCount = 0;
                var unloadedCount = 0;
                renderer.UnhandledException += (_, args) =>
                {
                    rendererExceptionCount++;
                    hostException = args.ExceptionObject as Exception;
                };
                host.Loaded += (_, _) => loadedCount++;
                host.Unloaded += (_, _) => unloadedCount++;
                host.HostError += (_, args) =>
                {
                    hostErrorCount++;
                    hostException = args.ExceptionObject as Exception;
                };
                secondXamlHost.HostError += (_, args) =>
                {
                    hostErrorCount++;
                    hostException = args.ExceptionObject as Exception;
                };

                var pageContent = new Grid();
                pageContent.Children.Add(new TextBlock { Text = "Native XAML content" });
                pageContent.Children.Add(host);
                pageContent.Children.Add(secondXamlHost);

                var missingRendererHost = new BlazorComponentHost
                {
                    ComponentType = typeof(TextRootComponent)
                };
                missingRendererHost.HostError += (_, args) =>
                {
                    hostErrorCount++;
                    hostException = args.ExceptionObject as Exception;
                };
                pageContent.Children.Add(missingRendererHost);

                window = new Window { Content = pageContent };
                window.Activate();

                await WaitForAsync(
                    () => host.IsLoaded && secondXamlHost.IsLoaded && rootPanel.Children.Count == 1 &&
                        Assert.IsType<Grid>(secondXamlHost.Content).Children.Count == 1 &&
                        missingRendererHost.IsLoaded && hostErrorCount == 1,
                    "The XAML hosts did not render their initial components.");
                Assert.Same(renderer, GetActiveHostRenderer(host));
                Assert.Same(renderer, GetActiveHostRenderer(secondXamlHost));
                var otherRenderer = new WinUIRenderer(
                    services,
                    dispatcherQueue,
                    Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory.Instance);
                Assert.Throws<InvalidOperationException>(() => host.Renderer = otherRenderer);
                Assert.Same(renderer, host.Renderer);
                Assert.IsType<InvalidOperationException>(hostException);
                Assert.Equal(0, rendererExceptionCount);
                Assert.Equal("Initial", Assert.IsType<TextBlock>(rootPanel.Children[0]).Text);
                var secondRootPanel = Assert.IsType<Grid>(secondXamlHost.Content);
                Assert.Equal("Shared renderer", Assert.IsType<TextBlock>(Assert.Single(secondRootPanel.Children)).Text);

                host.Parameters = new Dictionary<string, object?> { ["Text"] = "Updated" };
                await WaitForAsync(
                    () => rootPanel.Children.Count == 1 && rootPanel.Children[0] is TextBlock textBlock && textBlock.Text == "Updated",
                    "Replacing the parameter source did not update the root component.");

                host.ComponentType = typeof(ButtonRootComponent);
                host.Parameters = new Dictionary<string, object?> { ["Text"] = "Native button" };
                await WaitForAsync(
                    () => rootPanel.Children.Count == 1 && rootPanel.Children[0] is Button button && Equals(button.Content, "Native button"),
                    "Changing the component type did not replace the mounted root.");

                secondXamlHost.ComponentType = typeof(ThrowingRootComponent);
                await WaitForAsync(
                    () => rendererExceptionCount == 1,
                    "The renderer did not report a component exception once for its shared roots.");
                Assert.Equal(1, rendererExceptionCount);
                Assert.Equal(1, hostErrorCount);
                Assert.IsType<InvalidOperationException>(hostException);

                pageContent.Children.Remove(host);
                await WaitForAsync(
                    () => !host.IsLoaded && rootPanel.Children.Count == 0 && IsHostUnmounted(host),
                    "Unloading the XAML host did not remove its rendered controls.");

                pageContent.Children.Add(host);
                await WaitForAsync(
                    () => host.IsLoaded && rootPanel.Children.Count == 1 && rootPanel.Children[0] is Button,
                    "Reloading the XAML host did not mount a fresh root.");
                Assert.Same(renderer, GetActiveHostRenderer(host));

                window.Content = new Grid();
                await WaitForAsync(
                    () => !host.IsLoaded && !secondXamlHost.IsLoaded && rootPanel.Children.Count == 0 &&
                        Assert.IsType<Grid>(secondXamlHost.Content).Children.Count == 0 &&
                        !missingRendererHost.IsLoaded && IsHostUnmounted(host) &&
                        IsHostUnmounted(secondXamlHost) && IsHostUnmounted(missingRendererHost),
                    "The reloaded XAML hosts did not unmount cleanly.");
                Assert.Equal(0, GetNativeControlCount(renderer));
                Assert.Equal(2, loadedCount);
                Assert.Equal(2, unloadedCount);
            }
            finally
            {
                if (window is not null)
                {
                    window.Content = new Grid();
                    await Task.Delay(100);
                    window.Close();
                }

                await services.DisposeAsync();
            }
        });
    }

    [Fact]
    public void DispatcherInvocationsRunOnTheUiThreadFromBothCallers()
    {
        RunOnXamlThreadAsync(async () =>
        {
            var dispatcherQueue = DispatcherQueue.GetForCurrentThread()
                ?? throw new InvalidOperationException("The test thread has no WinUI DispatcherQueue.");
            var services = new ServiceCollection()
                .AddBlazorWinUI(dispatcherQueue)
                .BuildServiceProvider();
            var dispatcher = services.GetRequiredService<WinUIRenderer>().Dispatcher;
            var uiThreadId = Environment.CurrentManagedThreadId;

            var inlineActionThreadId = -1;
            await dispatcher.InvokeAsync(() => inlineActionThreadId = Environment.CurrentManagedThreadId);
            var inlineFunctionThreadId = await dispatcher.InvokeAsync(() => Environment.CurrentManagedThreadId);
            var inlineAsyncActionThreadId = -1;
            await dispatcher.InvokeAsync(async () =>
            {
                await Task.Yield();
                inlineAsyncActionThreadId = Environment.CurrentManagedThreadId;
            });
            var inlineAsyncFunctionThreadId = await dispatcher.InvokeAsync(async () =>
            {
                await Task.Yield();
                return Environment.CurrentManagedThreadId;
            });

            var queuedActionThreadId = -1;
            await Task.Run(() => dispatcher.InvokeAsync(() => queuedActionThreadId = Environment.CurrentManagedThreadId));
            var queuedFunctionThreadId = await Task.Run(() => dispatcher.InvokeAsync(() => Environment.CurrentManagedThreadId));
            var queuedAsyncActionThreadId = -1;
            await Task.Run(() => dispatcher.InvokeAsync(async () =>
            {
                await Task.Yield();
                queuedAsyncActionThreadId = Environment.CurrentManagedThreadId;
            }));
            var queuedAsyncFunctionThreadId = await Task.Run(() => dispatcher.InvokeAsync(async () =>
            {
                await Task.Yield();
                return Environment.CurrentManagedThreadId;
            }));

            Assert.Equal(uiThreadId, inlineActionThreadId);
            Assert.Equal(uiThreadId, inlineFunctionThreadId);
            Assert.Equal(uiThreadId, inlineAsyncActionThreadId);
            Assert.Equal(uiThreadId, inlineAsyncFunctionThreadId);
            Assert.Equal(uiThreadId, queuedActionThreadId);
            Assert.Equal(uiThreadId, queuedFunctionThreadId);
            Assert.Equal(uiThreadId, queuedAsyncActionThreadId);
            Assert.Equal(uiThreadId, queuedAsyncFunctionThreadId);

            await services.DisposeAsync();
        });
    }

    [Fact]
    public void DispatcherTasksFaultWhenItsQueueRejectsWork()
    {
        RunOnXamlThreadAsync(async () =>
        {
            var controller = DispatcherQueueController.CreateOnDedicatedThread();
            var stoppedQueue = controller.DispatcherQueue;
            await controller.ShutdownQueueAsync();

            var services = new ServiceCollection().BuildServiceProvider();
            var renderer = new WinUIRenderer(
                services,
                stoppedQueue,
                Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory.Instance);
            var dispatcher = renderer.Dispatcher;

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await dispatcher.InvokeAsync((Action)(() => { })));
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await dispatcher.InvokeAsync(() => Task.CompletedTask));
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await dispatcher.InvokeAsync(() => 1));
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await dispatcher.InvokeAsync(() => Task.FromResult(1)));

            await services.DisposeAsync();
        });
    }

    [Fact]
    public void AdaptersPreserveParameterEventAndContainerBehavior()
    {
        RunOnXamlThread(() =>
        {
            using (var adapter = new TextBlockAdapter())
            {
                var control = Assert.IsType<TextBlock>(adapter.Element);
                AssertAppliesAndResets(adapter, "Text", "Updated", () => control.Text, "Updated");
            }

            using (var adapter = new ProgressBarAdapter())
            {
                var control = Assert.IsType<ProgressBar>(adapter.Element);
                AssertAppliesAndResets(adapter, "Value", 0.5d, () => control.Value, 0.5d);
            }

            using (var adapter = new TextBoxAdapter())
            {
                var control = Assert.IsType<TextBox>(adapter.Element);
                AssertAppliesAndResets(adapter, "Text", "typed", () => control.Text, "typed");

                var callbackResult = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
                control.Text = "callback value";
                adapter.ApplyParameters(Parameters(("TextChanged", EventCallback.Factory.Create<string>(
                    new object(), value => callbackResult.TrySetResult(value)))));
                InvokeAdapterHandler(adapter, "OnTextChanged", control, null);
                Assert.True(callbackResult.Task.Wait(TimeSpan.FromSeconds(2)), "TextChanged callback was not forwarded.");
                Assert.Equal("callback value", callbackResult.Task.Result);
            }

            using (var adapter = new RichEditBoxAdapter())
            {
                var control = Assert.IsType<RichEditBox>(adapter.Element);
                AssertAppliesAndResets(adapter, "Text", "document text", () => ReadRichText(control), "document text");
            }

            using (var adapter = new PasswordBoxAdapter())
            {
                var control = Assert.IsType<PasswordBox>(adapter.Element);
                AssertAppliesAndResets(adapter, "Password", "secret", () => control.Password, "secret");
            }

            using (var adapter = new CheckBoxAdapter())
            {
                var control = Assert.IsType<CheckBox>(adapter.Element);
                AssertAppliesAndResets(adapter, "Text", "Agree", () => control.Content, (object)"Agree");

                var callbackResult = new TaskCompletionSource<bool?>(TaskCreationOptions.RunContinuationsAsynchronously);
                adapter.ApplyParameters(Parameters(("IsCheckedChanged", EventCallback.Factory.Create<bool?>(
                    new object(), value => callbackResult.TrySetResult(value)))));
                control.IsChecked = true;
                Assert.True(callbackResult.Task.Wait(TimeSpan.FromSeconds(2)), "CheckBox callback was not forwarded.");
                Assert.True(callbackResult.Task.Result);

                var checkedChangedCount = 0;
                adapter.ApplyParameters(Parameters(("IsCheckedChanged", EventCallback.Factory.Create<bool?>(
                    new object(), _ => { checkedChangedCount++; }))));
                control.IsChecked = false;
                Assert.Equal(1, checkedChangedCount);
                adapter.Dispose();
                control.IsChecked = true;
                Assert.Equal(1, checkedChangedCount);
            }

            using (var adapter = new RadioButtonAdapter())
            {
                var control = Assert.IsType<RadioButton>(adapter.Element);
                AssertAppliesAndResets(adapter, "Text", "Choice", () => control.Content, (object)"Choice");
            }

            using (var adapter = new ToggleSwitchAdapter())
            {
                var control = Assert.IsType<ToggleSwitch>(adapter.Element);
                AssertAppliesAndResets(adapter, "IsOn", true, () => control.IsOn, true);
            }

            using (var adapter = new SliderAdapter())
            {
                var control = Assert.IsType<Slider>(adapter.Element);
                AssertAppliesAndResets(adapter, "Value", 33d, () => control.Value, 33d);

                var callbackResult = new TaskCompletionSource<double>(TaskCreationOptions.RunContinuationsAsynchronously);
                adapter.ApplyParameters(Parameters(("ValueChanged", EventCallback.Factory.Create<double>(
                    new object(), value => callbackResult.TrySetResult(value)))));
                control.Value = 47d;
                Assert.True(callbackResult.Task.Wait(TimeSpan.FromSeconds(2)), "Slider callback was not forwarded.");
                Assert.Equal(47d, callbackResult.Task.Result);
            }

            using (var adapter = new StackPanelAdapter())
            {
                var control = Assert.IsType<StackPanel>(adapter.Element);
                AssertAppliesAndResets(adapter, "Spacing", 13d, () => control.Spacing, 13d);

                using var firstChild = new TextBlockAdapter();
                using var secondChild = new ButtonAdapter();
                adapter.SetChildren([firstChild.Element, secondChild.Element]);
                Assert.Equal(2, control.Children.Count);
                Assert.Same(firstChild.Element, control.Children[0]);
                Assert.Same(secondChild.Element, control.Children[1]);
            }

            using (var adapter = new GridAdapter())
            {
                var control = Assert.IsType<Grid>(adapter.Element);
                AssertAppliesAndResets(adapter, "RowSpacing", 8d, () => control.RowSpacing, 8d);
            }

            using (var adapter = new BorderAdapter())
            {
                var control = Assert.IsType<Border>(adapter.Element);
                var child = new TextBlock();
                adapter.SetChildren([child]);
                Assert.Same(child, control.Child);
                Assert.Throws<InvalidOperationException>(() => adapter.SetChildren([child, new TextBlock()]));
            }

            using (var adapter = new ScrollViewerAdapter())
            {
                var control = Assert.IsType<ScrollViewer>(adapter.Element);
                AssertAppliesAndResets(
                    adapter,
                    "VerticalScrollBarVisibility",
                    ScrollBarVisibility.Disabled,
                    () => control.VerticalScrollBarVisibility,
                    ScrollBarVisibility.Disabled);
            }

            using (var adapter = new ContentControlAdapter())
            {
                var control = Assert.IsType<ContentControl>(adapter.Element);
                AssertAppliesAndResets(adapter, "Padding", new Thickness(4), () => control.Padding, new Thickness(4));
            }

            using (var adapter = new ButtonAdapter())
            {
                var control = Assert.IsType<Button>(adapter.Element);
                AssertAppliesAndResets(adapter, "Text", "Action", () => control.Content, (object)"Action");

                var callbackInvoked = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                adapter.ApplyParameters(Parameters(("OnClick", EventCallback.Factory.Create(
                    new object(), () => callbackInvoked.TrySetResult(true)))));
                InvokeAdapterHandler(adapter, "OnOnClick", control, new RoutedEventArgs());
                Assert.True(callbackInvoked.Task.Wait(TimeSpan.FromSeconds(2)), "Button callback was not forwarded.");

                Assert.Throws<InvalidOperationException>(() => adapter.SetChildren([new TextBlock(), new TextBlock()]));

            }

            using (var adapter = new InfoBarAdapter())
            {
                var control = Assert.IsType<InfoBar>(adapter.Element);
                AssertAppliesAndResets(adapter, "Title", "Status", () => control.Title, "Status");
            }

            using (var adapter = new ComboBoxAdapter())
            {
                var control = Assert.IsType<ComboBox>(adapter.Element);
                using var firstItem = new ComboBoxItemAdapter();
                using var secondItem = new ComboBoxItemAdapter();
                adapter.SetChildren([firstItem.Element, secondItem.Element]);
                Assert.Equal(2, control.Items.Count);
                AssertAppliesAndResets(adapter, "SelectedIndex", 1, () => control.SelectedIndex, 1);
            }

            using (var adapter = new ComboBoxItemAdapter())
            {
                var control = Assert.IsType<ComboBoxItem>(adapter.Element);
                AssertAppliesAndResets(adapter, "Text", "Item", () => control.Content, (object)"Item");
            }

            using (var adapter = new ListViewAdapter())
            {
                var control = Assert.IsType<ListView>(adapter.Element);
                using var item = new ListViewItemAdapter();
                adapter.SetChildren([item.Element]);
                AssertAppliesAndResets(adapter, "SelectedIndex", 0, () => control.SelectedIndex, 0);
            }

            using (var adapter = new ListViewItemAdapter())
            {
                var control = Assert.IsType<ListViewItem>(adapter.Element);
                AssertAppliesAndResets(adapter, "Text", "Row", () => control.Content, (object)"Row");
            }

            using (var adapter = new NavigationViewAdapter())
            using (var itemAdapter = new NavigationViewItemAdapter())
            {
                var control = Assert.IsType<NavigationView>(adapter.Element);
                using var contentAdapter = new TextBlockAdapter();
                adapter.SetChildren([itemAdapter.Element, contentAdapter.Element]);
                Assert.Single(control.MenuItems);
                Assert.Same(itemAdapter.Element, control.MenuItems[0]);
                Assert.Same(contentAdapter.Element, control.Content);
                Assert.Throws<InvalidOperationException>(() => adapter.SetChildren([
                    itemAdapter.Element,
                    new TextBlock(),
                    new TextBlock()]));
            }

            using (var adapter = new NavigationViewItemSeparatorAdapter())
            {
                Assert.IsType<NavigationViewItemSeparator>(adapter.Element);
            }

            using (var adapter = new ComboBoxAdapter())
            {
                adapter.ApplyParameters(Parameters(("ItemsSource", new object[] { "one" })));
                Assert.Throws<InvalidOperationException>(() => adapter.SetChildren([new TextBlock()]));
            }
        });
    }

    private static void AssertAppliesAndResets<T>(IAdapter adapter, string parameterName, object? value, Func<T> read, T expected)
    {
        var initial = read();
        adapter.ApplyParameters(Parameters((parameterName, value)));
        Assert.Equal(expected, read());
        adapter.ApplyParameters(ParameterView.Empty);
        Assert.Equal(initial, read());
    }

    private static ParameterView Parameters(params (string Name, object? Value)[] values)
    {
        return ParameterView.FromDictionary(values.ToDictionary(pair => pair.Name, pair => pair.Value, StringComparer.Ordinal));
    }

    private static string ReadRichText(RichEditBox control)
    {
        control.Document.GetText(TextGetOptions.None, out var text);
        return text.TrimEnd('\r');
    }

    private static void InvokeAdapterHandler(IAdapter adapter, string methodName, params object?[] arguments)
    {
        var handler = adapter.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic)
            ?? throw new InvalidOperationException($"Could not find adapter handler {methodName}.");
        handler.Invoke(adapter, arguments);
    }

    private static void RunOnXamlThread(Action action)
    {
        Exception? failure = null;
        using var completed = new ManualResetEventSlim();
        var thread = new Thread(() =>
        {
            try
            {
                var bootstrapAssembly = Assembly.Load("Microsoft.WindowsAppRuntime.Bootstrap.Net");
                var bootstrapType = bootstrapAssembly.GetType("Microsoft.Windows.ApplicationModel.DynamicDependency.Bootstrap", throwOnError: true)!;
                bootstrapType.GetMethod("Initialize", [typeof(uint)])!.Invoke(null, [0x00020005u]);

                Application.Start(_ =>
                {
                    try
                    {
                        new Application();
                        action();
                    }
                    catch (Exception exception)
                    {
                        failure = exception;
                    }
                    finally
                    {
                        Application.Current?.Exit();
                    }
                });
            }
            catch (Exception exception)
            {
                failure = exception;
            }
            finally
            {
                completed.Set();
            }
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(completed.Wait(TimeSpan.FromSeconds(30)), "WinUI failed to start on the test thread.");
        thread.Join();
        if (failure is not null)
        {
            ExceptionDispatchInfo.Capture(failure).Throw();
        }
    }

    private static void RunOnXamlThreadAsync(Func<Task> action)
    {
        Exception? failure = null;
        using var completed = new ManualResetEventSlim();
        var thread = new Thread(() =>
        {
            try
            {
                var bootstrapAssembly = Assembly.Load("Microsoft.WindowsAppRuntime.Bootstrap.Net");
                var bootstrapType = bootstrapAssembly.GetType("Microsoft.Windows.ApplicationModel.DynamicDependency.Bootstrap", throwOnError: true)!;
                bootstrapType.GetMethod("Initialize", [typeof(uint)])!.Invoke(null, [0x00020005u]);

                Application.Start(initializationParams =>
                {
                    try
                    {
                        new Application();
                        var dispatcherQueue = DispatcherQueue.GetForCurrentThread()
                            ?? throw new InvalidOperationException("The test thread has no WinUI DispatcherQueue.");
                        SynchronizationContext.SetSynchronizationContext(new TestDispatcherSynchronizationContext(dispatcherQueue));
                        _ = RunActionAsync();
                    }
                    catch (Exception exception)
                    {
                        failure = exception;
                        Application.Current?.Exit();
                    }
                });
            }
            catch (Exception exception)
            {
                failure = exception;
            }
            finally
            {
                completed.Set();
            }

            async Task RunActionAsync()
            {
                try
                {
                    await action();
                }
                catch (Exception exception)
                {
                    failure = exception;
                }
                finally
                {
                    Application.Current?.Exit();
                }
            }
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(completed.Wait(TimeSpan.FromSeconds(30)), "WinUI failed to stop the asynchronous test thread.");
        thread.Join();
        if (failure is not null)
        {
            ExceptionDispatchInfo.Capture(failure).Throw();
        }
    }

    private static Task WaitForAsync(Func<bool> condition, string failureMessage)
    {
        return WaitForAsync(condition, () => failureMessage);
    }

    private static async Task WaitForAsync(Func<bool> condition, Func<string> failureMessage)
    {
        var timeout = DateTime.UtcNow + TimeSpan.FromSeconds(10);
        while (DateTime.UtcNow < timeout)
        {
            if (condition())
            {
                return;
            }

            await Task.Delay(20);
        }

        Assert.True(condition(), failureMessage());
    }

    private static bool IsHostUnmounted(BlazorComponentHost host)
    {
        var rootComponentId = typeof(BlazorComponentHost)
            .GetField("_rootComponentId", BindingFlags.NonPublic | BindingFlags.Instance)!
            .GetValue(host);
        var renderer = typeof(BlazorComponentHost)
            .GetField("_mountedRenderer", BindingFlags.NonPublic | BindingFlags.Instance)!
            .GetValue(host);
        return rootComponentId is null && renderer is null;
    }

    private static WinUIRenderer? GetActiveHostRenderer(BlazorComponentHost host)
    {
        return (WinUIRenderer?)typeof(BlazorComponentHost)
            .GetField("_mountedRenderer", BindingFlags.NonPublic | BindingFlags.Instance)!
            .GetValue(host);
    }

    private static int GetNativeControlCount(WinUIRenderer renderer)
    {
        var nativeControls = typeof(WinUIRenderer)
            .GetProperty("NativeControls", BindingFlags.NonPublic | BindingFlags.Instance)!
            .GetValue(renderer)!;
        return (int)nativeControls.GetType().GetProperty("Count")!.GetValue(nativeControls)!;
    }

    public sealed class TextRootComponent : ComponentBase, IDisposable
    {
        public static int DisposedCount { get; private set; }

        [Parameter]
        public string? Text { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<BlazorWinUI.Components.TextBlock>(0);
            builder.AddAttribute(1, nameof(BlazorWinUI.Components.TextBlock.Text), Text);
            builder.CloseComponent();
        }

        public void Dispose()
        {
            DisposedCount++;
        }
    }

    public sealed class ButtonRootComponent : ComponentBase
    {
        [Parameter]
        public string? Text { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<BlazorWinUI.Components.Button>(0);
            builder.AddAttribute(1, nameof(BlazorWinUI.Components.Button.Text), Text);
            builder.CloseComponent();
        }
    }

    public sealed class ThrowingRootComponent : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            throw new InvalidOperationException("The root component failed during rendering.");
        }
    }

    private sealed class TestDispatcherSynchronizationContext(DispatcherQueue dispatcherQueue) : SynchronizationContext
    {
        public override void Post(SendOrPostCallback callback, object? state)
        {
            if (!dispatcherQueue.TryEnqueue(() => callback(state)))
            {
                throw new InvalidOperationException("The WinUI DispatcherQueue stopped before the test continuation ran.");
            }
        }

        public override void Send(SendOrPostCallback callback, object? state)
        {
            if (dispatcherQueue.HasThreadAccess)
            {
                callback(state);
                return;
            }

            using var completed = new ManualResetEventSlim();
            Exception? failure = null;
            if (!dispatcherQueue.TryEnqueue(() =>
            {
                try
                {
                    callback(state);
                }
                catch (Exception exception)
                {
                    failure = exception;
                }
                finally
                {
                    completed.Set();
                }
            }))
            {
                throw new InvalidOperationException("The WinUI DispatcherQueue stopped before the test callback ran.");
            }

            completed.Wait();
            if (failure is not null)
            {
                ExceptionDispatchInfo.Capture(failure).Throw();
            }
        }
    }
}
