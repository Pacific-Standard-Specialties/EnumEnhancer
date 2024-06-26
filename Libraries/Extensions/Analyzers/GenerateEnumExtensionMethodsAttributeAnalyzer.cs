﻿#define JETBRAINS_ANNOTATIONS
using System.Collections.Immutable;

using EnumEnhancer.Attributes;
using EnumEnhancer.Generators.EnumExtensions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace EnumEnhancer.Analyzers;

/// <summary>
///     Design-time analyzer that checks for proper use of <see cref="GenerateEnumExtensionMethodsAttribute"/>.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
[UsedImplicitly]
internal sealed class GenerateEnumExtensionMethodsAttributeAnalyzer : DiagnosticAnalyzer
{
    // ReSharper disable once InconsistentNaming
    private static readonly DiagnosticDescriptor EE0001_GlobalNamespaceNotSupported =
        new(
            // ReSharper restore InconsistentNaming
            "EE0001",
            $"{nameof(GenerateEnumExtensionMethodsAttribute)} not supported on global enums",
            "{0} is in the global namespace, which is not supported by the source generator ({1}) used by {2}. Move the enum to a namespace or remove the attribute.",
            "Usage",
            DiagnosticSeverity.Error,
            true,
            null,
            null,
            WellKnownDiagnosticTags.NotConfigurable,
            WellKnownDiagnosticTags.Compiler);

    // ReSharper disable once InconsistentNaming
    private static readonly DiagnosticDescriptor EE0002_UnderlyingTypeNotSupported =
        new(
            "EE0002",
            $"{nameof(GenerateEnumExtensionMethodsAttribute)} not supported for this enum type",
            "{0} has an underlying type of {1}, which is not supported by the source generator ({2}) used by {3}. Only enums backed by int or uint are supported.",
            "Usage",
            DiagnosticSeverity.Error,
            true,
            null,
            null,
            WellKnownDiagnosticTags.NotConfigurable,
            WellKnownDiagnosticTags.Compiler);

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        [
            EE0001_GlobalNamespaceNotSupported,
            EE0002_UnderlyingTypeNotSupported
        ];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(CheckAttributeLocations, SyntaxKind.EnumDeclaration);

        return;

        static void CheckAttributeLocations(SyntaxNodeAnalysisContext analysisContext)
        {
            ISymbol? symbol = analysisContext.SemanticModel.GetDeclaredSymbol(analysisContext.Node) as INamedTypeSymbol;

            if (symbol is not INamedTypeSymbol { EnumUnderlyingType: { } } enumSymbol)
            {
                // Somehow not even an enum declaration.
                // Skip it.
                return;
            }

            // Check attributes for those we care about and react accordingly.
            foreach (AttributeData attributeData in enumSymbol.GetAttributes())
            {
                if (attributeData.AttributeClass?.Name != nameof(GenerateEnumExtensionMethodsAttribute))
                {
                    // Just skip - not an interesting attribute.
                    continue;
                }

                // Check enum underlying type for supported types (int and uint, currently)
                // Report EE0002 if unsupported underlying type.
                if (enumSymbol.EnumUnderlyingType is not { SpecialType: SpecialType.System_Int32 or SpecialType.System_UInt32 })
                {
                    analysisContext.ReportDiagnostic(
                                                      Diagnostic.Create(
                                                                         EE0002_UnderlyingTypeNotSupported,
                                                                         enumSymbol.Locations.FirstOrDefault(),
                                                                         enumSymbol.Name,
                                                                         enumSymbol.EnumUnderlyingType.Name,
                                                                         nameof(EnumExtensionMethodsIncrementalGenerator),
                                                                         nameof(GenerateEnumExtensionMethodsAttribute)
                                                                        )
                                                     );
                }

                // Check enum namespace (only non-global supported, currently)
                // Report EE0001 if in the global namespace.
                if (enumSymbol.ContainingSymbol is not INamespaceSymbol { IsGlobalNamespace: false })
                {
                    analysisContext.ReportDiagnostic(
                                                      Diagnostic.Create(
                                                                         EE0001_GlobalNamespaceNotSupported,
                                                                         enumSymbol.Locations.FirstOrDefault(),
                                                                         enumSymbol.Name,
                                                                         nameof(EnumExtensionMethodsIncrementalGenerator),
                                                                         nameof(GenerateEnumExtensionMethodsAttribute)
                                                                        )
                                                     );
                }
            }
        }
    }
}
