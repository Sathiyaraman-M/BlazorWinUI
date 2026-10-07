using System.Reflection;
using System.Text.Json;

using BlazorWinUI;

using Microsoft.AspNetCore.Components;

using Xunit;

namespace BlazorWinUI.Tests;

public sealed class ComponentApiSnapshotTests
{
    private static readonly IReadOnlyDictionary<string, string> PrimitiveAliases = new Dictionary<string, string>(StringComparer.Ordinal)
    {
        ["System.Boolean"] = "bool",
        ["System.Byte"] = "byte",
        ["System.Char"] = "char",
        ["System.Decimal"] = "decimal",
        ["System.Double"] = "double",
        ["System.Int16"] = "short",
        ["System.Int32"] = "int",
        ["System.Int64"] = "long",
        ["System.Object"] = "object",
        ["System.SByte"] = "sbyte",
        ["System.Single"] = "float",
        ["System.String"] = "string",
        ["System.UInt16"] = "ushort",
        ["System.UInt32"] = "uint",
        ["System.UInt64"] = "ulong"
    };

    [Fact]
    public void ComponentsPreserveTheGeneratedParameterSurface()
    {
        var baseline = ReadSnapshot();
        var library = typeof(WinUIRenderer).Assembly;
        var nullability = new NullabilityInfoContext();

        Assert.Equal(23, baseline.Count);

        foreach (var (componentName, expectedParameters) in baseline)
        {
            var componentType = library.GetType("BlazorWinUI.Components." + componentName, throwOnError: true)!;
            var actualParameters = componentType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(property => property.GetCustomAttribute<ParameterAttribute>() is not null)
                .ToDictionary(property => property.Name, StringComparer.Ordinal);

            Assert.Equal(
                expectedParameters.Select(parameter => parameter.Name).Order(StringComparer.Ordinal).ToArray(),
                actualParameters.Keys.Order(StringComparer.Ordinal).ToArray());

            foreach (var expected in expectedParameters)
            {
                Assert.Equal(
                    NormalizeExpectedType(expected.Type),
                    DisplayType(actualParameters[expected.Name].PropertyType, nullability.Create(actualParameters[expected.Name])));
            }
        }
    }

    [Fact]
    public void DefaultResolverRegistersAnAdapterForEveryBuiltInComponent()
    {
        var library = typeof(WinUIRenderer).Assembly;
        var resolverType = library.GetType("BlazorWinUI.AdapterResolver", throwOnError: true)!;
        var resolver = resolverType
            .GetMethod("CreateDefault", BindingFlags.Public | BindingFlags.Static)!
            .Invoke(null, [new EmptyServiceProvider()])!;
        var hasAdapter = resolverType.GetMethod("HasAdapter", BindingFlags.Public | BindingFlags.Instance)!;

        foreach (var componentName in ReadSnapshot().Keys)
        {
            var componentType = library.GetType("BlazorWinUI.Components." + componentName, throwOnError: true)!;
            var registered = (bool)hasAdapter.Invoke(resolver, [componentType])!;
            Assert.True(registered, $"No default adapter was registered for {componentName}.");
        }
    }

    private static Dictionary<string, List<ParameterSnapshot>> ReadSnapshot()
    {
        var resourceName = typeof(ComponentApiSnapshotTests).Assembly
            .GetManifestResourceNames()
            .Single(name => name.EndsWith("ComponentApiSnapshot.json", StringComparison.Ordinal));

        using var stream = typeof(ComponentApiSnapshotTests).Assembly.GetManifestResourceStream(resourceName)!;
        return JsonSerializer.Deserialize<Dictionary<string, List<ParameterSnapshot>>>(
            stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? throw new InvalidOperationException("The component API snapshot was empty.");
    }

    private static string NormalizeExpectedType(string type)
    {
        return type.Replace("global::System.String", "string", StringComparison.Ordinal);
    }

    private static string DisplayType(Type type, NullabilityInfo? nullability)
    {
        var nullableValueType = Nullable.GetUnderlyingType(type);
        if (nullableValueType is not null)
        {
            return DisplayType(nullableValueType, null) + "?";
        }

        string displayName;
        if (type.IsArray)
        {
            displayName = DisplayType(type.GetElementType()!, nullability?.ElementType) + "[]";
        }
        else if (type.IsGenericType)
        {
            var definition = type.GetGenericTypeDefinition().FullName!;
            var arity = definition.IndexOf('`');
            if (arity >= 0)
            {
                definition = definition[..arity];
            }

            definition = definition.Replace('+', '.');
            var arguments = type.GetGenericArguments();
            var argumentNullability = nullability?.GenericTypeArguments ?? [];
            displayName = "global::" + definition + "<" + string.Join(
                ", ",
                arguments.Select((argument, index) => DisplayType(
                    argument,
                    index < argumentNullability.Length ? argumentNullability[index] : null))) + ">";
        }
        else
        {
            var fullName = type.FullName ?? type.Name;
            displayName = PrimitiveAliases.TryGetValue(fullName, out var alias)
                ? alias
                : "global::" + fullName.Replace('+', '.');
        }

        if (!type.IsValueType && nullability?.ReadState == NullabilityState.Nullable)
        {
            displayName += "?";
        }

        return displayName;
    }

    private sealed record ParameterSnapshot(string Name, string Type);

    private sealed class EmptyServiceProvider : IServiceProvider
    {
        public object? GetService(Type serviceType) => null;
    }
}
