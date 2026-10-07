namespace BlazorWinUI;

internal static class DefaultAdapterRegistry
{
    internal static void RegisterDefaults(AdapterResolver resolver)
    {
        resolver.Register<Components.StackPanel, Adapters.StackPanelAdapter>();
        resolver.Register<Components.Grid, Adapters.GridAdapter>();
        resolver.Register<Components.Border, Adapters.BorderAdapter>();
        resolver.Register<Components.ScrollViewer, Adapters.ScrollViewerAdapter>();
        resolver.Register<Components.ContentControl, Adapters.ContentControlAdapter>();
        resolver.Register<Components.TextBlock, Adapters.TextBlockAdapter>();
        resolver.Register<Components.Button, Adapters.ButtonAdapter>();
        resolver.Register<Components.TextBox, Adapters.TextBoxAdapter>();
        resolver.Register<Components.RichEditBox, Adapters.RichEditBoxAdapter>();
        resolver.Register<Components.PasswordBox, Adapters.PasswordBoxAdapter>();
        resolver.Register<Components.CheckBox, Adapters.CheckBoxAdapter>();
        resolver.Register<Components.RadioButton, Adapters.RadioButtonAdapter>();
        resolver.Register<Components.ToggleSwitch, Adapters.ToggleSwitchAdapter>();
        resolver.Register<Components.Slider, Adapters.SliderAdapter>();
        resolver.Register<Components.ProgressBar, Adapters.ProgressBarAdapter>();
        resolver.Register<Components.InfoBar, Adapters.InfoBarAdapter>();
        resolver.Register<Components.ComboBox, Adapters.ComboBoxAdapter>();
        resolver.Register<Components.ComboBoxItem, Adapters.ComboBoxItemAdapter>();
        resolver.Register<Components.NavigationView, Adapters.NavigationViewAdapter>();
        resolver.Register<Components.NavigationViewItem, Adapters.NavigationViewItemAdapter>();
        resolver.Register<Components.NavigationViewItemSeparator, Adapters.NavigationViewItemSeparatorAdapter>();
        resolver.Register<Components.ListView, Adapters.ListViewAdapter>();
        resolver.Register<Components.ListViewItem, Adapters.ListViewItemAdapter>();
    }
}
