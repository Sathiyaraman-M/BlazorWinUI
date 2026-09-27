using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace BlazorWinUI.Generators;

[Generator(LanguageNames.CSharp)]
public sealed class WinUIControlGenerator : IIncrementalGenerator
{
    private static readonly DiagnosticDescriptor MissingControl = new(
        "BLWGEN001",
        "WinUI control is unavailable",
        "The pinned WinUI references do not contain '{0}'; its Blazor wrapper was not generated",
        "BlazorWinUI.Generation",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor MissingProperty = new(
        "BLWGEN002",
        "WinUI property is unavailable",
        "The pinned WinUI control '{0}' does not expose a public readable and writable '{1}' property; parameter '{2}' was not generated",
        "BlazorWinUI.Generation",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor MissingEvent = new(
        "BLWGEN003",
        "WinUI event is unavailable",
        "The pinned WinUI control '{0}' does not expose a '{1}' event; callback '{2}' was not generated",
        "BlazorWinUI.Generation",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly ControlDefinition[] Controls =
    [
        new("StackPanel", ContainerKind.Panel,
            [P("Orientation"), P("Spacing")]),
        new("Grid", ContainerKind.Panel,
            [P("RowSpacing"), P("ColumnSpacing"), P("Padding")]),
        new("Border", ContainerKind.Child,
            [P("Background"), P("BorderBrush"), P("BorderThickness"), P("CornerRadius"), P("Padding")]),
        new("ScrollViewer", ContainerKind.Content,
            [P("HorizontalScrollBarVisibility"), P("VerticalScrollBarVisibility"), P("HorizontalScrollMode"), P("VerticalScrollMode"), P("ZoomMode"), P("IsEnabled")]),
        new("ContentControl", ContainerKind.Content,
            [P("HorizontalContentAlignment"), P("VerticalContentAlignment"), P("Padding"), P("IsEnabled")]),
        new("TextBlock", ContainerKind.None,
            [P("Text"), P("FontSize"), P("FontFamily"), P("FontWeight"), P("Foreground"), P("TextAlignment"), P("TextWrapping"), P("TextTrimming"), P("IsTextSelectionEnabled"), P("MaxLines")]),
        new("Button", ContainerKind.Content,
            [P("Text", "Content", "global::System.String?"), P("IsEnabled"), P("HorizontalAlignment"), P("VerticalAlignment")],
            [E("OnClick", "global::Microsoft.AspNetCore.Components.EventCallback", "Click", "global::Microsoft.UI.Xaml.RoutedEventArgs", "null")]),
        new("TextBox", ContainerKind.None,
            [P("Text"), P("PlaceholderText"), P("IsReadOnly"), P("MaxLength"), P("AcceptsReturn"), P("TextWrapping"), P("TextAlignment"), P("IsSpellCheckEnabled"), P("IsEnabled")],
            [E("TextChanged", "global::Microsoft.AspNetCore.Components.EventCallback<string>", "TextChanged", "global::Microsoft.UI.Xaml.Controls.TextChangedEventArgs", "_control.Text")]),
        new("RichEditBox", ContainerKind.None,
            [P("Text", componentType: "global::System.String?", adapterAssignment: "_control.Document.GetText(global::Microsoft.UI.Text.TextGetOptions.None, out var currentText);\nvar value = (string?)parameter.Value ?? string.Empty;\nif (!global::System.String.Equals(currentText, value, global::System.StringComparison.Ordinal)) _control.Document.SetText(global::Microsoft.UI.Text.TextSetOptions.None, value);", adapterReset: "_control.Document.SetText(global::Microsoft.UI.Text.TextSetOptions.None, _defaultText ?? string.Empty);", defaultCapture: "_control.Document.GetText(global::Microsoft.UI.Text.TextGetOptions.None, out var initialText);\n_defaultText = initialText;")],
            [EWithBody("TextChanged", "global::Microsoft.AspNetCore.Components.EventCallback<string>", "TextChanged", "global::Microsoft.UI.Xaml.RoutedEventArgs", "_control.Document.GetText(global::Microsoft.UI.Text.TextGetOptions.None, out var text);\nawait _textChanged.InvokeAsync(text);")]),
        new("PasswordBox", ContainerKind.None,
            [P("Password"), P("PlaceholderText"), P("MaxLength"), P("PasswordChar"), P("IsPasswordRevealButtonEnabled"), P("IsEnabled")],
            [E("PasswordChanged", "global::Microsoft.AspNetCore.Components.EventCallback<string>", "PasswordChanged", "global::Microsoft.UI.Xaml.RoutedEventArgs", "_control.Password")]),
        new("CheckBox", ContainerKind.None,
            [P("Text", "Content", "global::System.String?"), P("IsChecked"), P("IsThreeState"), P("IsEnabled")],
            [E("IsCheckedChanged", "global::Microsoft.AspNetCore.Components.EventCallback<bool?>", ["Checked", "Unchecked", "Indeterminate"], "global::Microsoft.UI.Xaml.RoutedEventArgs", "_control.IsChecked")]),
        new("RadioButton", ContainerKind.None,
            [P("Text", "Content", "global::System.String?"), P("IsChecked"), P("GroupName"), P("IsEnabled")],
            [E("IsCheckedChanged", "global::Microsoft.AspNetCore.Components.EventCallback<bool?>", ["Checked", "Unchecked"], "global::Microsoft.UI.Xaml.RoutedEventArgs", "_control.IsChecked")]),
        new("ToggleSwitch", ContainerKind.None,
            [P("IsOn"), P("OnContent"), P("OffContent"), P("IsEnabled")],
            [E("IsOnChanged", "global::Microsoft.AspNetCore.Components.EventCallback<bool>", "Toggled", "global::Microsoft.UI.Xaml.RoutedEventArgs", "_control.IsOn")]),
        new("Slider", ContainerKind.None,
            [P("Minimum"), P("Maximum"), P("Value"), P("StepFrequency"), P("TickFrequency"), P("IsThumbToolTipEnabled"), P("Orientation"), P("IsEnabled")],
            [E("ValueChanged", "global::Microsoft.AspNetCore.Components.EventCallback<double>", "ValueChanged", "global::Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs", "args.NewValue")]),
        new("ProgressBar", ContainerKind.None,
            [P("Minimum"), P("Maximum"), P("Value"), P("IsIndeterminate"), P("ShowError"), P("ShowPaused"), P("IsEnabled")]),
        new("InfoBar", ContainerKind.Content,
            [P("Title"), P("Message"), P("IsOpen"), P("IsClosable"), P("IsIconVisible"), P("Severity"), P("IsEnabled")],
            [E("OnClosed", "global::Microsoft.AspNetCore.Components.EventCallback", "Closed", "global::Microsoft.UI.Xaml.Controls.InfoBarClosedEventArgs", "null")]),
        new("ComboBox", ContainerKind.Items,
            [P("PlaceholderText"), P("IsEditable"), P("IsDropDownOpen"), P("IsTextSearchEnabled"), P("MaxDropDownHeight"), P("SelectedIndex"), P("IsEnabled")],
            [E("SelectedIndexChanged", "global::Microsoft.AspNetCore.Components.EventCallback<int>", "SelectionChanged", "global::Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs", "_control.SelectedIndex")]),
        new("ComboBoxItem", ContainerKind.Content,
            [P("Text", "Content", "global::System.String?"), P("IsSelected"), P("IsEnabled")]),
        new("ListView", ContainerKind.Items,
            [P("SelectedIndex"), P("SelectionMode"), P("IsItemClickEnabled"), P("IsEnabled")],
            [E("SelectedIndexChanged", "global::Microsoft.AspNetCore.Components.EventCallback<int>", "SelectionChanged", "global::Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs", "_control.SelectedIndex")]),
        new("ListViewItem", ContainerKind.Content,
            [P("Text", "Content", "global::System.String?"), P("IsSelected"), P("IsEnabled")])
    ];

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(
            context.CompilationProvider,
            static (productionContext, compilation) => Generate(productionContext, compilation));
    }

    private static void Generate(SourceProductionContext context, Compilation compilation)
    {
        var generatedControls = new List<GeneratedControl>();

        foreach (var definition in Controls)
        {
            var nativeType = compilation.GetTypeByMetadataName(
                "Microsoft.UI.Xaml.Controls." + definition.NativeName);

            if (nativeType is null)
            {
                context.ReportDiagnostic(Diagnostic.Create(MissingControl, Location.None, definition.NativeName));
                continue;
            }

            var properties = new List<PropertyDefinition>();
            var parameterNames = new HashSet<string>(StringComparer.Ordinal);
            var nativePropertyNames = new HashSet<string>(StringComparer.Ordinal);
            foreach (var mapping in definition.Properties)
            {
                if (mapping.AdapterAssignment is not null)
                {
                    properties.Add(new PropertyDefinition(
                        mapping.ParameterName,
                        mapping.NativeName,
                        mapping.ComponentType!,
                        mapping.ComponentType!,
                        initializeToDefault: false,
                        adapterAssignment: mapping.AdapterAssignment,
                        adapterReset: mapping.AdapterReset,
                        defaultCapture: mapping.DefaultCapture));
                    parameterNames.Add(mapping.ParameterName);
                    continue;
                }

                var property = FindProperty(nativeType, mapping.NativeName);
                if (property is null)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        MissingProperty,
                        Location.None,
                        definition.NativeName,
                        mapping.NativeName,
                        mapping.ParameterName));
                    continue;
                }

                properties.Add(new PropertyDefinition(
                    mapping.ParameterName,
                    mapping.NativeName,
                    mapping.ComponentType ?? DisplayType(property.Type),
                    DisplayType(property.Type),
                    mapping.ComponentType is null && property.Type.IsReferenceType &&
                    property.NullableAnnotation != NullableAnnotation.Annotated,
                    adapterAssignment: null));
                parameterNames.Add(mapping.ParameterName);
                nativePropertyNames.Add(mapping.NativeName);
            }

            foreach (var property in FindWritableProperties(nativeType))
            {
                if (nativePropertyNames.Contains(property.Name) ||
                    parameterNames.Contains(property.Name) ||
                    IsRendererOwnedProperty(definition.Container, property.Name) ||
                    HasObsoleteAttribute(property) ||
                    !CanExposeType(property.Type))
                {
                    continue;
                }

                properties.Add(new PropertyDefinition(
                    property.Name,
                    property.Name,
                    DisplayType(property.Type),
                    DisplayType(property.Type),
                    property.Type.IsReferenceType && property.NullableAnnotation != NullableAnnotation.Annotated));
                parameterNames.Add(property.Name);
                nativePropertyNames.Add(property.Name);
            }

            var events = new List<EventDefinition>();
            foreach (var eventDefinition in definition.Events)
            {
                var existingEventNames = eventDefinition.NativeNames
                    .Where(name => FindEvent(nativeType, name) is not null)
                    .ToArray();

                if (existingEventNames.Length == 0)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        MissingEvent,
                        Location.None,
                        definition.NativeName,
                        eventDefinition.NativeNames[0],
                        eventDefinition.ParameterName));
                    continue;
                }

                events.Add(new EventDefinition(
                    eventDefinition.ParameterName,
                    eventDefinition.CallbackType,
                    existingEventNames,
                    eventDefinition.HandlerParameters,
                    eventDefinition.ValueExpression,
                    eventDefinition.HandlerBody));
            }

            var overriddenEventNames = new HashSet<string>(
                definition.Events.SelectMany(eventDefinition => eventDefinition.NativeNames),
                StringComparer.Ordinal);
            foreach (var eventSymbol in FindEvents(nativeType))
            {
                if (overriddenEventNames.Contains(eventSymbol.Name) || HasObsoleteAttribute(eventSymbol))
                {
                    continue;
                }

                var invokeMethod = (eventSymbol.Type as INamedTypeSymbol)?.DelegateInvokeMethod;
                if (invokeMethod is null || !invokeMethod.ReturnsVoid ||
                    invokeMethod.Parameters.Any(parameter =>
                        parameter.RefKind != RefKind.None || !CanExposeType(parameter.Type)))
                {
                    continue;
                }

                var handlerParameters = invokeMethod.Parameters
                    .Select((parameter, index) => DisplayType(parameter.Type) + " arg" + index)
                    .ToArray();
                var callbackName = "On" + eventSymbol.Name;
                if (!parameterNames.Add(callbackName))
                {
                    continue;
                }

                var callbackType = invokeMethod.Parameters.Length == 0
                    ? "global::Microsoft.AspNetCore.Components.EventCallback"
                    : "global::Microsoft.AspNetCore.Components.EventCallback<" +
                      DisplayType(invokeMethod.Parameters[invokeMethod.Parameters.Length - 1].Type) + ">";
                var valueExpression = invokeMethod.Parameters.Length == 0
                    ? "null"
                    : "arg" + (invokeMethod.Parameters.Length - 1);

                events.Add(new EventDefinition(
                    callbackName,
                    callbackType,
                    [eventSymbol.Name],
                    string.Join(", ", handlerParameters),
                    valueExpression,
                    handlerBody: null));
            }

            generatedControls.Add(new GeneratedControl(definition, properties, events));
        }

        foreach (var control in generatedControls)
        {
            var name = control.Definition.NativeName;
            if (compilation.GetTypeByMetadataName("BlazorWinUI.Components." + name) is null)
            {
                context.AddSource(
                    name + ".Component.g.cs",
                    SourceText.From(GenerateComponent(control), Encoding.UTF8));
            }

            if (compilation.GetTypeByMetadataName("BlazorWinUI.Adapters." + name + "Adapter") is null)
            {
                context.AddSource(
                    name + ".Adapter.g.cs",
                    SourceText.From(GenerateAdapter(control), Encoding.UTF8));
            }
        }

        context.AddSource(
            "GeneratedAdapterRegistry.g.cs",
            SourceText.From(GenerateRegistry(generatedControls), Encoding.UTF8));
    }

    private static IPropertySymbol? FindProperty(INamedTypeSymbol type, string name)
    {
        for (var current = type; current is not null; current = current.BaseType)
        {
            var property = current.GetMembers(name).OfType<IPropertySymbol>().FirstOrDefault();
            if (property?.GetMethod?.DeclaredAccessibility == Accessibility.Public &&
                property.SetMethod?.DeclaredAccessibility == Accessibility.Public &&
                !property.GetMethod.IsStatic &&
                !property.SetMethod.IsStatic &&
                !property.IsIndexer)
            {
                return property;
            }
        }

        return null;
    }

    private static IEnumerable<IPropertySymbol> FindWritableProperties(INamedTypeSymbol type)
    {
        var seenNames = new HashSet<string>(StringComparer.Ordinal);
        for (var current = type; current is not null; current = current.BaseType)
        {
            foreach (var property in current.GetMembers().OfType<IPropertySymbol>())
            {
                if (property.GetMethod?.DeclaredAccessibility == Accessibility.Public &&
                    property.SetMethod?.DeclaredAccessibility == Accessibility.Public &&
                    !property.GetMethod.IsStatic &&
                    !property.SetMethod.IsStatic &&
                    !property.IsIndexer &&
                    seenNames.Add(property.Name))
                {
                    yield return property;
                }
            }
        }
    }

    private static IEventSymbol? FindEvent(INamedTypeSymbol type, string name)
    {
        for (var current = type; current is not null; current = current.BaseType)
        {
            var eventSymbol = current.GetMembers(name).OfType<IEventSymbol>().FirstOrDefault();
            if (eventSymbol?.DeclaredAccessibility == Accessibility.Public && !eventSymbol.IsStatic)
            {
                return eventSymbol;
            }
        }

        return null;
    }

    private static IEnumerable<IEventSymbol> FindEvents(INamedTypeSymbol type)
    {
        var seenNames = new HashSet<string>(StringComparer.Ordinal);
        for (var current = type; current is not null; current = current.BaseType)
        {
            foreach (var eventSymbol in current.GetMembers().OfType<IEventSymbol>())
            {
                if (eventSymbol.DeclaredAccessibility == Accessibility.Public &&
                    !eventSymbol.IsStatic &&
                    seenNames.Add(eventSymbol.Name))
                {
                    yield return eventSymbol;
                }
            }
        }
    }

    private static bool IsRendererOwnedProperty(ContainerKind container, string propertyName)
    {
        return container switch
        {
            ContainerKind.Panel => propertyName == "Children",
            ContainerKind.Items => propertyName == "Items",
            _ => false
        };
    }

    private static bool HasObsoleteAttribute(ISymbol symbol)
    {
        return symbol.GetAttributes().Any(attribute =>
            attribute.AttributeClass?.ToDisplayString() == "System.ObsoleteAttribute");
    }

    private static bool CanExposeType(ITypeSymbol type)
    {
        if (type.TypeKind is TypeKind.Error or TypeKind.Pointer or TypeKind.FunctionPointer or TypeKind.TypeParameter)
        {
            return false;
        }

        if (type is IArrayTypeSymbol array)
        {
            return CanExposeType(array.ElementType);
        }

        if (type is INamedTypeSymbol namedType)
        {
            if (namedType.IsRefLikeType)
            {
                return false;
            }

            for (var current = namedType; current is not null; current = current.ContainingType)
            {
                if (current.DeclaredAccessibility != Accessibility.Public)
                {
                    return false;
                }
            }

            return namedType.TypeArguments.All(CanExposeType);
        }

        return type.SpecialType != SpecialType.None || type.TypeKind == TypeKind.Dynamic;
    }

    private static string GenerateComponent(GeneratedControl control)
    {
        var componentName = control.Definition.NativeName;
        var source = new StringBuilder();
        source.AppendLine("// <auto-generated />");
        source.AppendLine("#nullable enable");
        source.AppendLine("namespace BlazorWinUI.Components;");
        source.AppendLine();
        source.Append("public sealed class ").Append(componentName).AppendLine(" : global::Microsoft.AspNetCore.Components.ComponentBase");
        source.AppendLine("{");

        foreach (var property in control.Properties)
        {
            source.AppendLine("    [global::Microsoft.AspNetCore.Components.ParameterAttribute]");
            source.Append("    public ").Append(property.ComponentType).Append(' ').Append(property.ParameterName)
                .Append(" { get; set; }");
            if (property.InitializeToDefault)
            {
                source.AppendLine(" = default!;");
            }
            else
            {
                source.AppendLine();
            }
        }

        foreach (var eventDefinition in control.Events)
        {
            source.AppendLine("    [global::Microsoft.AspNetCore.Components.ParameterAttribute]");
            source.Append("    public ").Append(eventDefinition.CallbackType).Append(' ').Append(eventDefinition.ParameterName)
                .AppendLine(" { get; set; }");
        }

        if (control.Definition.Container != ContainerKind.None)
        {
            source.AppendLine("    [global::Microsoft.AspNetCore.Components.ParameterAttribute]");
            source.AppendLine("    public global::Microsoft.AspNetCore.Components.RenderFragment? ChildContent { get; set; }");
            source.AppendLine();
            source.AppendLine("    protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)");
            source.AppendLine("    {");
            source.AppendLine("        builder.AddContent(0, ChildContent);");
            source.AppendLine("    }");
        }

        source.AppendLine("}");
        return source.ToString();
    }

    private static string GenerateAdapter(GeneratedControl control)
    {
        var definition = control.Definition;
        var className = definition.NativeName + "Adapter";
        var source = new StringBuilder();
        source.AppendLine("// <auto-generated />");
        source.AppendLine("#nullable enable");
        source.AppendLine("namespace BlazorWinUI.Adapters;");
        source.AppendLine();
        source.Append("public sealed class ").Append(className).Append(" : global::BlazorWinUI.Abstractions.IAdapter");
        if (definition.Container != ContainerKind.None)
        {
            source.Append(", global::BlazorWinUI.Abstractions.IControlContainer");
        }

        source.AppendLine();
        source.AppendLine("{");
        source.Append("    private readonly global::Microsoft.UI.Xaml.Controls.").Append(definition.NativeName)
            .Append(" _control = new global::Microsoft.UI.Xaml.Controls.").Append(definition.NativeName).AppendLine("();");

        foreach (var eventDefinition in control.Events)
        {
            source.Append("    private ").Append(eventDefinition.CallbackType).Append(" _").Append(FieldName(eventDefinition.ParameterName)).AppendLine(";");
        }

        source.AppendLine("    private readonly global::System.Collections.Generic.HashSet<string> _presentParameters = new(global::System.StringComparer.Ordinal);");
        foreach (var property in control.Properties)
        {
            if (property.AdapterAssignment is not null && property.DefaultCapture is null)
            {
                continue;
            }

            source.Append("    private readonly ").Append(property.NativeType).Append(" _default")
                .Append(property.ParameterName).AppendLine(";");
        }

        var contentPropertyName = SingleContentProperty(definition.Container);
        if (contentPropertyName is not null)
        {
            source.AppendLine("    private object? _configuredContent;");
            source.AppendLine("    private bool _hasRenderedChildren;");
        }

        source.AppendLine();
        source.AppendLine("    public global::Microsoft.UI.Xaml.FrameworkElement Element => _control;");

        if (control.Events.Count > 0 || control.Properties.Count > 0)
        {
            source.AppendLine();
            source.Append("    public ").Append(className).AppendLine("()");
            source.AppendLine("    {");
            foreach (var property in control.Properties)
            {
                if (property.DefaultCapture is not null)
                {
                    foreach (var line in property.DefaultCapture.Split('\n'))
                    {
                        source.Append("        ").AppendLine(line.TrimEnd('\r'));
                    }
                }
                else if (property.AdapterAssignment is null)
                {
                    source.Append("        _default").Append(property.ParameterName).Append(" = _control.")
                        .Append(property.NativeName).AppendLine(";");
                }
            }

            if (contentPropertyName is not null)
            {
                var content = control.Properties.FirstOrDefault(property => property.NativeName == contentPropertyName);
                if (content is not null)
                {
                    source.Append("        _configuredContent = _default").Append(content.ParameterName).AppendLine(";");
                }
            }

            foreach (var eventDefinition in control.Events)
            {
                foreach (var nativeEvent in eventDefinition.NativeNames)
                {
                    source.Append("        _control.").Append(nativeEvent).Append(" += On").Append(eventDefinition.ParameterName).AppendLine(";");
                }
            }

            source.AppendLine("    }");
        }

        source.AppendLine();
        source.AppendLine("    public void ApplyParameters(global::Microsoft.AspNetCore.Components.ParameterView parameterView)");
        source.AppendLine("    {");
        source.AppendLine("        var currentParameters = new global::System.Collections.Generic.HashSet<string>(global::System.StringComparer.Ordinal);");
        source.AppendLine("        foreach (var parameter in parameterView)");
        source.AppendLine("        {");
        source.AppendLine("            currentParameters.Add(parameter.Name);");
        source.AppendLine("            switch (parameter.Name)");
        source.AppendLine("            {");

        foreach (var property in control.Properties)
        {
            if (property.AdapterAssignment is not null)
            {
                source.Append("                case \"").Append(property.ParameterName).AppendLine("\":");
                source.AppendLine("                {");
                foreach (var line in property.AdapterAssignment.Split('\n'))
                {
                    source.Append("                    ").AppendLine(line.TrimEnd('\r'));
                }

                source.AppendLine("                    break;");
                source.AppendLine("                }");
            }
            else if (contentPropertyName == property.NativeName)
            {
                source.Append("                case \"").Append(property.ParameterName).AppendLine("\":");
                source.AppendLine("                {");
                source.AppendLine("                    _configuredContent = parameter.Value;");
                source.Append("                    if (!_hasRenderedChildren) _control.").Append(property.NativeName)
                    .Append(" = (").Append(property.NativeType).AppendLine(")parameter.Value!;");
                source.AppendLine("                    break;");
                source.AppendLine("                }");
            }
            else
            {
                source.Append("                case \"").Append(property.ParameterName).Append("\": _control.")
                    .Append(property.NativeName).Append(" = (").Append(property.ComponentType).AppendLine(")parameter.Value!; break;");
            }
        }

        foreach (var eventDefinition in control.Events)
        {
            source.Append("                case \"").Append(eventDefinition.ParameterName).Append("\": _")
                .Append(FieldName(eventDefinition.ParameterName)).Append(" = (").Append(eventDefinition.CallbackType)
                .AppendLine(")parameter.Value!; break;");
        }

        source.AppendLine("            }");
        source.AppendLine("        }");
        source.AppendLine("        foreach (var removedParameter in _presentParameters)");
        source.AppendLine("        {");
        source.AppendLine("            if (currentParameters.Contains(removedParameter)) continue;");
        source.AppendLine("            switch (removedParameter)");
        source.AppendLine("            {");

        foreach (var property in control.Properties)
        {
            source.Append("                case \"").Append(property.ParameterName).AppendLine("\":");
            source.AppendLine("                {");
            if (property.AdapterReset is not null)
            {
                foreach (var line in property.AdapterReset.Split('\n'))
                {
                    source.Append("                    ").AppendLine(line.TrimEnd('\r'));
                }
            }
            else if (contentPropertyName == property.NativeName)
            {
                source.Append("                    _configuredContent = _default").Append(property.ParameterName).AppendLine(";");
                source.Append("                    if (!_hasRenderedChildren) _control.").Append(property.NativeName)
                    .Append(" = _default").Append(property.ParameterName).AppendLine(";");
            }
            else if (property.AdapterAssignment is null)
            {
                source.Append("                    _control.").Append(property.NativeName).Append(" = _default")
                    .Append(property.ParameterName).AppendLine(";");
            }

            source.AppendLine("                    break;");
            source.AppendLine("                }");
        }

        foreach (var eventDefinition in control.Events)
        {
            source.Append("                case \"").Append(eventDefinition.ParameterName).Append("\": _")
                .Append(FieldName(eventDefinition.ParameterName)).AppendLine(" = default; break;");
        }

        source.AppendLine("            }");
        source.AppendLine("        }");
        source.AppendLine("        _presentParameters.Clear();");
        source.AppendLine("        _presentParameters.UnionWith(currentParameters);");
        source.AppendLine("    }");

        foreach (var eventDefinition in control.Events)
        {
            source.AppendLine();
            source.Append("    private async void On").Append(eventDefinition.ParameterName).Append('(')
                .Append(eventDefinition.HandlerParameters).AppendLine(")");
            source.AppendLine("    {");
            if (eventDefinition.HandlerBody is not null)
            {
                foreach (var line in eventDefinition.HandlerBody.Split('\n'))
                {
                    source.Append("        ").AppendLine(line.TrimEnd('\r'));
                }
            }
            else
            {
                source.Append("        await _").Append(FieldName(eventDefinition.ParameterName)).Append(".InvokeAsync(");
                if (eventDefinition.ValueExpression != "null")
                {
                    source.Append(eventDefinition.ValueExpression);
                }

                source.AppendLine(");");
            }

            source.AppendLine("    }");
        }

        if (definition.Container != ContainerKind.None)
        {
            source.AppendLine();
            source.AppendLine("    public void SetChildren(global::System.Collections.Generic.IReadOnlyList<global::Microsoft.UI.Xaml.FrameworkElement> children)");
            source.AppendLine("    {");
            switch (definition.Container)
            {
                case ContainerKind.Panel:
                    source.AppendLine("        _control.Children.Clear();");
                    source.AppendLine("        foreach (var child in children) _control.Children.Add(child);");
                    break;
                case ContainerKind.Content:
                    AppendSingleContent(source, "Content", control.Properties.FirstOrDefault(property => property.NativeName == "Content")?.NativeType ?? "global::System.Object?");
                    break;
                case ContainerKind.Child:
                    AppendSingleContent(source, "Child", control.Properties.FirstOrDefault(property => property.NativeName == "Child")?.NativeType ?? "global::Microsoft.UI.Xaml.UIElement?");
                    break;
                case ContainerKind.Items:
                    source.AppendLine("        if (_control.ItemsSource is not null)");
                    source.AppendLine("        {");
                    source.AppendLine("            if (children.Count > 0) throw new global::System.InvalidOperationException(\"ItemsSource and child components cannot be used together.\");");
                    source.AppendLine("            return;");
                    source.AppendLine("        }");
                    source.AppendLine("        _control.Items.Clear();");
                    source.AppendLine("        foreach (var child in children) _control.Items.Add(child);");
                    break;
            }

            source.AppendLine("    }");
        }

        source.AppendLine();
        source.AppendLine("    public void Dispose()");
        source.AppendLine("    {");
        foreach (var eventDefinition in control.Events)
        {
            foreach (var nativeEvent in eventDefinition.NativeNames)
            {
                source.Append("        _control.").Append(nativeEvent).Append(" -= On").Append(eventDefinition.ParameterName).AppendLine(";");
            }
        }

        source.AppendLine("    }");
        source.AppendLine("}");
        return source.ToString();
    }

    private static void AppendSingleContent(StringBuilder source, string propertyName, string propertyType)
    {
        source.AppendLine("        if (children.Count > 1)");
        source.AppendLine("        {");
        source.AppendLine("            throw new global::System.InvalidOperationException(\"This WinUI control accepts at most one child component.\");");
        source.AppendLine("        }");
        source.AppendLine("        _hasRenderedChildren = children.Count > 0;");
        source.Append("        _control.").Append(propertyName).Append(" = children.Count == 0 ? (").Append(propertyType)
            .AppendLine(")_configuredContent! : children[0];");
    }

    private static string? SingleContentProperty(ContainerKind container)
    {
        return container switch
        {
            ContainerKind.Content => "Content",
            ContainerKind.Child => "Child",
            _ => null
        };
    }

    private static string GenerateRegistry(IReadOnlyList<GeneratedControl> controls)
    {
        var source = new StringBuilder();
        source.AppendLine("// <auto-generated />");
        source.AppendLine("namespace BlazorWinUI;");
        source.AppendLine();
        source.AppendLine("internal static class GeneratedAdapterRegistry");
        source.AppendLine("{");
        source.AppendLine("    internal static void RegisterDefaults(AdapterResolver resolver)");
        source.AppendLine("    {");
        foreach (var control in controls)
        {
            var componentName = control.Definition.NativeName;
            source.Append("        resolver.Register<global::BlazorWinUI.Components.").Append(componentName)
                .Append(", global::BlazorWinUI.Adapters.").Append(componentName).AppendLine("Adapter>();");
        }

        source.AppendLine("    }");
        source.AppendLine("}");
        return source.ToString();
    }

    private static string DisplayType(ITypeSymbol type)
    {
        return type.ToDisplayString(
        SymbolDisplayFormat.FullyQualifiedFormat.WithMiscellaneousOptions(
            SymbolDisplayFormat.FullyQualifiedFormat.MiscellaneousOptions |
            SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier));
    }

    private static string FieldName(string parameterName)
    {
        return char.ToLowerInvariant(parameterName[0]) + parameterName.Substring(1);
    }

    private static PropertyMapping P(
        string name,
        string? nativeName = null,
        string? componentType = null,
        string? adapterAssignment = null,
        string? adapterReset = null,
        string? defaultCapture = null)
    {
        return new(name, nativeName ?? name, componentType, adapterAssignment, adapterReset, defaultCapture);
    }

    private static EventMapping E(
        string parameterName,
        string callbackType,
        string nativeName,
        string eventArgsType,
        string valueExpression)
    {
        return new(parameterName, callbackType, [nativeName], eventArgsType, valueExpression);
    }

    private static EventMapping E(
        string parameterName,
        string callbackType,
        string[] nativeNames,
        string eventArgsType,
        string valueExpression)
    {
        return new(parameterName, callbackType, nativeNames, eventArgsType, valueExpression);
    }

    private static EventMapping EWithBody(
        string parameterName,
        string callbackType,
        string nativeName,
        string eventArgsType,
        string handlerBody)
    {
        return new(parameterName, callbackType, [nativeName], eventArgsType, "null", handlerBody);
    }

    private enum ContainerKind
    {
        None,
        Panel,
        Content,
        Child,
        Items
    }

    private sealed class ControlDefinition(
        string nativeName,
        ContainerKind container,
        PropertyMapping[] properties,
        EventMapping[]? events = null)
    {
        public string NativeName { get; } = nativeName;
        public ContainerKind Container { get; } = container;
        public PropertyMapping[] Properties { get; } = properties;
        public EventMapping[] Events { get; } = events ?? [];
    }

    private sealed class PropertyMapping(
        string parameterName,
        string nativeName,
        string? componentType,
        string? adapterAssignment,
        string? adapterReset,
        string? defaultCapture)
    {
        public string ParameterName { get; } = parameterName;
        public string NativeName { get; } = nativeName;
        public string? ComponentType { get; } = componentType;
        public string? AdapterAssignment { get; } = adapterAssignment;
        public string? AdapterReset { get; } = adapterReset;
        public string? DefaultCapture { get; } = defaultCapture;
    }

    private sealed class EventMapping(
        string parameterName,
        string callbackType,
        string[] nativeNames,
        string eventArgsType,
        string valueExpression,
        string? handlerBody = null)
    {
        public string ParameterName { get; } = parameterName;
        public string CallbackType { get; } = callbackType;
        public string[] NativeNames { get; } = nativeNames;
        public string HandlerParameters { get; } = "object sender, " + eventArgsType + " args";
        public string ValueExpression { get; } = valueExpression;
        public string? HandlerBody { get; } = handlerBody;
    }

    private sealed class GeneratedControl(
        ControlDefinition definition,
        IReadOnlyList<PropertyDefinition> properties,
        IReadOnlyList<EventDefinition> events)
    {
        public ControlDefinition Definition { get; } = definition;
        public IReadOnlyList<PropertyDefinition> Properties { get; } = properties;
        public IReadOnlyList<EventDefinition> Events { get; } = events;
    }

    private sealed class PropertyDefinition(
        string parameterName,
        string nativeName,
        string componentType,
        string nativeType,
        bool initializeToDefault,
        string? adapterAssignment = null,
        string? adapterReset = null,
        string? defaultCapture = null)
    {
        public string ParameterName { get; } = parameterName;
        public string NativeName { get; } = nativeName;
        public string ComponentType { get; } = componentType;
        public string NativeType { get; } = nativeType;
        public bool InitializeToDefault { get; } = initializeToDefault;
        public string? AdapterAssignment { get; } = adapterAssignment;
        public string? AdapterReset { get; } = adapterReset;
        public string? DefaultCapture { get; } = defaultCapture;
    }

    private sealed class EventDefinition(
        string parameterName,
        string callbackType,
        string[] nativeNames,
        string handlerParameters,
        string valueExpression,
        string? handlerBody)
    {
        public string ParameterName { get; } = parameterName;
        public string CallbackType { get; } = callbackType;
        public string[] NativeNames { get; } = nativeNames;
        public string HandlerParameters { get; } = handlerParameters;
        public string ValueExpression { get; } = valueExpression;
        public string? HandlerBody { get; } = handlerBody;
    }
}
