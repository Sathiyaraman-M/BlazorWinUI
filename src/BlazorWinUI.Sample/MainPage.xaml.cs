using BlazorWinUI;

using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BlazorWinUI_Sample;

/// <summary>
/// The main content page displayed inside the application window.
/// Add your UI logic, event handlers, and data binding here.
/// </summary>
public sealed partial class MainPage : Page
{
    private readonly DispatcherQueue _dispatcherQueue;
    private bool _isRendererExceptionHandlerRegistered;

    public WinUIRenderer Renderer => ((App)Application.Current).Renderer;

    public Type RootComponentType => typeof(RootComponent);

    public MainPage()
    {
        InitializeComponent();

        _dispatcherQueue = DispatcherQueue.GetForCurrentThread()
            ?? throw new InvalidOperationException("The sample page must be created on the WinUI UI thread.");
        Loaded += OnPageLoaded;
        Unloaded += OnPageUnloaded;
    }

    private void OnPageLoaded(object sender, RoutedEventArgs args)
    {
        if (_isRendererExceptionHandlerRegistered)
        {
            return;
        }

        Renderer.UnhandledException += OnRendererUnhandledException;
        _isRendererExceptionHandlerRegistered = true;
    }

    private void OnPageUnloaded(object sender, RoutedEventArgs args)
    {
        if (!_isRendererExceptionHandlerRegistered)
        {
            return;
        }

        Renderer.UnhandledException -= OnRendererUnhandledException;
        _isRendererExceptionHandlerRegistered = false;
    }

    private void RootHost_OnHostError(object? sender, System.UnhandledExceptionEventArgs e)
    {
        ShowException(e.ExceptionObject);
    }

    private void OnRendererUnhandledException(object? sender, System.UnhandledExceptionEventArgs e)
    {
        ShowException(e.ExceptionObject);
    }

    private void ShowException(object? exceptionObject)
    {
        var exception = exceptionObject as Exception
            ?? new Exception($"BlazorWinUI reported an unhandled exception: {exceptionObject}");

        if (_dispatcherQueue.HasThreadAccess)
        {
            _ = ShowRendererExceptionAsync(exception);
            return;
        }

        _dispatcherQueue.TryEnqueue(() => _ = ShowRendererExceptionAsync(exception));
    }

    private async Task ShowRendererExceptionAsync(Exception exception)
    {
        var dialog = new ContentDialog
        {
            Title = "Renderer exception",
            Content = exception.ToString(),
            CloseButtonText = "Close",
            XamlRoot = RootHost.XamlRoot
        };

        await dialog.ShowAsync();
    }
}
