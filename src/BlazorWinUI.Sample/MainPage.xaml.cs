using BlazorWinUI;
using BlazorWinUI.Adapters;
using BlazorWinUI.Components;
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

    public MainPage()
    {
        InitializeComponent();

        var services = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();

        _renderer = new WinUIRenderer(
            services,
            DispatcherQueue.GetForCurrentThread(),
            loggerFactory);
        _renderer.RegisterAdapter<BlazorWinUI.Components.StackPanel, StackPanelAdapter>();
        _renderer.RegisterAdapter<BlazorWinUI.Components.TextBlock, TextBlockAdapter>();
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Loaded -= OnLoaded;
        await _renderer.MountRootComponentAsync<RootComponent>(RootHost);
    }
}
