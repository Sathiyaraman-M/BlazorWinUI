using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;

using BlazorWinUI.Abstractions;
using BlazorWinUI.Adapters;

using Microsoft.AspNetCore.Components;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Xunit;

namespace BlazorWinUI.Tests;

public sealed class AdapterBehaviorTests
{
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
}
