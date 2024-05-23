using Microsoft.CodeAnalysis;

namespace EnumEnhancer;

internal static class AccessibilityExtensions
{
    internal static string ToCSharpString(this Accessibility value)
    {
        return value switch
        {
            Accessibility.Public => "public",
            Accessibility.Internal => "internal",
            Accessibility.Private => "private",
            Accessibility.Protected => "protected",
            Accessibility.ProtectedAndInternal => "private protected",
            Accessibility.ProtectedOrInternal => "protected internal",
            _ => string.Empty
        };
    }
}
