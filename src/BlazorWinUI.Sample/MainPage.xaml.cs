using BlazorWinUI;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Dispatching;
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
    private readonly WinUIRenderer _renderer;
    private readonly DispatcherQueue _dispatcherQueue;

    public MainPage()
    {
        InitializeComponent();

        var services = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        _dispatcherQueue = DispatcherQueue.GetForCurrentThread();

        _renderer = new WinUIRenderer(
            services,
            _dispatcherQueue,
            loggerFactory);
        _renderer.OnUnhandledException += OnRendererUnhandledException;
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Loaded -= OnLoaded;
        await _renderer.MountRootComponentAsync<RootComponent>(RootHost);
    }

    private void OnRendererUnhandledException(object? sender, System.UnhandledExceptionEventArgs e)
    {
        var exception = e.ExceptionObject as Exception
            ?? new Exception($"Renderer reported an unhandled exception: {e.ExceptionObject}");

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
