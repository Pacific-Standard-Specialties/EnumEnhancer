// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable once RedundantNullableDirective
// ReSharper disable RedundantNameQualifier
#nullable enable

namespace EnumEnhancer.Attributes;

/// <summary>Assembly attribute declaring a known pairing of an <see langword="enum" /> type to an extension class.</summary>
/// <remarks>This attribute should only be written by internal source generators for EnumEnhancer. No other usage of any kind is supported.</remarks>
[System.AttributeUsage(System.AttributeTargets.Assembly, AllowMultiple = true)]
internal sealed class AssemblyExtendedEnumTypeAttribute : System.Attribute
{
    /// <summary>Creates a new instance of <see cref="AssemblyExtendedEnumTypeAttribute" /> from the provided parameters.</summary>
    /// <param name="enumType">The <see cref="System.Type" /> of an <see langword="enum" /> decorated with a <see cref="GenerateEnumExtensionMethodsAttribute" />.</param>
    /// <param name="extensionClass">The <see cref="System.Type" /> of the <see langword="class" /> decorated with an <see cref="ExtensionsForEnumTypeAttribute{TEnum}" /> referring to the same type as <paramref name="enumType" />.</param>
    public AssemblyExtendedEnumTypeAttribute(System.Type enumType, System.Type extensionClass)
    {
        EnumType = enumType;
        ExtensionClass = extensionClass;
    }
    ///<summary>An <see langword="enum" /> type that has been extended by EnumEnhancer source generators.</summary>
    public System.Type EnumType { get; }
    ///<summary>A class containing extension methods for <see cref="EnumType"/>.</summary>
    public System.Type ExtensionClass { get; }

    /// <inheritdoc />
    public override string ToString() => $"{EnumType.Name},{ExtensionClass.Name}";
}
