# BlazorWinUI

BlazorWinUI renders Razor components as native Windows App SDK controls. The library includes a compile-time generated set of Blazor components and adapters for the pinned WinUI SDK, so consumers do not need to register built-in controls or maintain a parallel property/event wrapper by hand.

## Example

```razor
@using BlazorWinUI.Components

<StackPanel Spacing="8">
    <TextBlock Text="Line1" />
    <TextBlock Text="Line2" />
    <Button Text="Increment" OnClick="IncrementCount" />
    <TextBlock Text="Count: @count" />
</StackPanel>

@code {
    private int count;

    private void IncrementCount() => count++;
}
```

## Included controls

The initial generated catalog covers:

- Layout and content: `StackPanel`, `Grid`, `Border`, `ScrollViewer`, `ContentControl`.
- Text and input: `TextBlock`, `TextBox`, `RichEditBox`, `PasswordBox`.
- Buttons and feedback: `Button`, `CheckBox`, `RadioButton`, `ToggleSwitch`, `ProgressBar`, `InfoBar`.
- Range and selection: `Slider`, `ComboBox`, `ComboBoxItem`, `ListView`, `ListViewItem`.

For each catalog control, the generator exposes its public, writable, non-obsolete WinUI properties whose types are accessible to the application. The control catalog and child-content policy are explicit; properties and compatible public events are discovered from the pinned Windows App SDK references at build time. New SDK properties/events therefore flow into the generated API when the library is rebuilt against that SDK, without hand-maintaining a wrapper for each member.

WinUI events are generally exposed as `On<EventName>` callbacks using the event delegate's last argument, if any. A small set of familiar Blazor conventions is mapped specially: `Button.OnClick`, `TextBox`/`RichEditBox` `Text` plus `TextChanged` for `@bind-Text`, and value/selection callbacks such as `Slider.ValueChanged` and `ComboBox.SelectedIndexChanged`.

## Content and collections

`Panel` controls accept multiple child components. Single-content controls accept at most one child component and may also take their native content parameter (`Text` is a string convenience for controls such as `Button`). `ComboBox` and `ListView` support either child item components or their native `ItemsSource`; setting both is rejected because the renderer does not provide an item-template bridge yet.

When a parameter is removed on a later render, the adapter restores the value captured from the newly created native control. This lets conditional Razor attributes and callbacks be removed without leaving stale native state behind.

## Custom adapters and current boundary

Built-in adapters are registered automatically. A renderer can replace a mapping with `RegisterAdapter<TComponent, TAdapter>()` before mounting the component. The adapter implements `IAdapter` and, for components that own children, `IControlContainer`.

This is a native-control renderer, not a web renderer: HTML elements, general item templates/virtualization, and automatic conversion for arbitrary object-valued WinUI properties are not provided. Use the exposed WinUI property types directly, and prefer child components for native visual content.
