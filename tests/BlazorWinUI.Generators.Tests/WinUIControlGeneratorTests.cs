using BlazorWinUI.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace BlazorWinUI.Generators.Tests;

public sealed class WinUIControlGeneratorTests
{
    private const string ButtonSource = """
        #nullable enable
        namespace Microsoft.UI.Xaml
        {
            public class FrameworkElement { }
            public class RoutedEventArgs : global::System.EventArgs { }
            public enum HorizontalAlignment { Left, Center, Right, Stretch }
            public enum VerticalAlignment { Top, Center, Bottom, Stretch }
        }

        namespace Microsoft.UI.Xaml.Controls
        {
            public class Button : global::Microsoft.UI.Xaml.FrameworkElement
            {
                public object? Content { get; set; }
                public bool IsEnabled { get; set; }
                public global::Microsoft.UI.Xaml.HorizontalAlignment HorizontalAlignment { get; set; }
                public global::Microsoft.UI.Xaml.VerticalAlignment VerticalAlignment { get; set; }
                public string? AutomationLabel { get; set; }
                public event global::System.EventHandler<global::Microsoft.UI.Xaml.RoutedEventArgs>? Click;
                public event global::System.EventHandler<global::Microsoft.UI.Xaml.RoutedEventArgs>? FocusRequested;
            }
        }
        """;

    [Fact]
    public void EmitsComponentAdapterAndDefaultRegistrationForWinUiButton()
    {
        var generated = Generate(ButtonSource);

        Assert.Contains("Button.Component.g.cs", generated.Keys);
        Assert.Contains("Button.Adapter.g.cs", generated.Keys);
        Assert.Contains("public global::System.String? Text", generated["Button.Component.g.cs"]);
        Assert.Contains("AutomationLabel", generated["Button.Component.g.cs"]);
        Assert.Contains("public global::Microsoft.AspNetCore.Components.EventCallback OnClick", generated["Button.Component.g.cs"]);
        Assert.Contains("public global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.UI.Xaml.RoutedEventArgs> OnFocusRequested", generated["Button.Component.g.cs"]);
        Assert.Contains("_control.Click += OnOnClick", generated["Button.Adapter.g.cs"]);
        Assert.Contains("_control.Click -= OnOnClick", generated["Button.Adapter.g.cs"]);
        Assert.Contains("_control.AutomationLabel = _defaultAutomationLabel", generated["Button.Adapter.g.cs"]);
        Assert.Contains("case \"Text\":", generated["Button.Adapter.g.cs"]);
        Assert.Contains("_control.Content = _defaultText", generated["Button.Adapter.g.cs"]);
        Assert.Contains("_onClick = default", generated["Button.Adapter.g.cs"]);
        Assert.Contains("_presentParameters.UnionWith(currentParameters)", generated["Button.Adapter.g.cs"]);
        Assert.Contains(
            "resolver.Register<global::BlazorWinUI.Components.Button, global::BlazorWinUI.Adapters.ButtonAdapter>()",
            generated["GeneratedAdapterRegistry.g.cs"]);

        Assert.All(generated.Values, source =>
            Assert.DoesNotContain(
                CSharpSyntaxTree.ParseText(source).GetDiagnostics(),
                diagnostic => diagnostic.Severity == DiagnosticSeverity.Error));
    }

    [Fact]
    public void ExistingComponentAndAdapterTypesOverrideGeneratedTypes()
    {
        const string overrides = """
            namespace BlazorWinUI.Components { public sealed class Button { } }
            namespace BlazorWinUI.Adapters { public sealed class ButtonAdapter { } }
            """;

        var generated = Generate(ButtonSource + overrides);

        Assert.DoesNotContain("Button.Component.g.cs", generated.Keys);
        Assert.DoesNotContain("Button.Adapter.g.cs", generated.Keys);
        Assert.Contains("GeneratedAdapterRegistry.g.cs", generated.Keys);
        Assert.Contains(
            "resolver.Register<global::BlazorWinUI.Components.Button, global::BlazorWinUI.Adapters.ButtonAdapter>()",
            generated["GeneratedAdapterRegistry.g.cs"]);
    }

    private static Dictionary<string, string> Generate(string source)
    {
        var references = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!)
            .Split(Path.PathSeparator)
            .Select(path => MetadataReference.CreateFromFile(path));
        var compilation = CSharpCompilation.Create(
            "WinUIControlGenerator.Tests.Input",
            [CSharpSyntaxTree.ParseText(source)],
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        GeneratorDriver driver = CSharpGeneratorDriver.Create(
            new WinUIControlGenerator().AsSourceGenerator());
        driver = driver.RunGenerators(compilation);

        return driver.GetRunResult().Results
            .SelectMany(result => result.GeneratedSources)
            .ToDictionary(sourceResult => sourceResult.HintName, sourceResult => sourceResult.SourceText.ToString());
    }
}
