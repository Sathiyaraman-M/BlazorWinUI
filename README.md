# BlazorWinUI

Project to build native windows applications using Blazor with a special WinUI-based Renderer. Inspired by the Flutter+Blazor demo (By Steve Sanderson demonstrated in NDC Oslo 2019) and Blazor-Binding projects. In other words, this project is basically Blazor-Bindings for WinUI.

## Install

Add the package to a .NET 10 WinUI project:

```bash
dotnet add package BlazorWinUI
```

> [!NOTE]
> Supports only .NET 10 and above.

To support having the razor files in your project, you have to make the following changes into your project file:
- Change your Project SDK from `Microsoft.NET.Sdk` to `Microsoft.NET.Sdk.Razor`
- Add the following `ItemGroup` to your project
```csproj
<ItemGroup>
  <Content Remove="**/*.razor" />
  <RazorComponent Include="**/*.razor" />
</ItemGroup>
```

Refer to the [sample project](src\BlazorWinUI.Sample) on how to setup the `WinUIRenderer` and mount blazor components.
