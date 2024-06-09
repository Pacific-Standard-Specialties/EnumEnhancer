namespace PSS.EnumEnhancers.ExtensionMethods.Attributes;

/// <summary>Assembly attribute declaring a known pairing of an <see langword="enum" /> type to a generated enum class.</summary>
/// <remarks>
///   This attribute should only be written by internal source generators for EnumEnhancer. No other usage of any
///   kind is supported.
/// </remarks>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
[JetBrains.Annotations.PublicAPI]
public sealed class AssemblyExtendedEnumTypeAttribute : Attribute
{
  /// <summary>Creates a new instance of <see cref="AssemblyExtendedEnumTypeAttribute" /> from the provided parameters.</summary>
  /// <param name="enumType">
  ///   The <see cref="Type" /> of an <see langword="enum" /> decorated with a
  ///   <see cref="GenerateEnumExtensionMethodsAttribute" />.
  /// </param>
  /// <param name="extensionClass">
  ///   The <see cref="Type" /> of the <see langword="class" /> decorated with an
  ///   <see cref="ExtensionsForEnumTypeAttribute{TEnum}" /> referring to the same type as <paramref name="enumType" />.
  /// </param>
  public AssemblyExtendedEnumTypeAttribute(Type enumType, Type extensionClass)
  {
    EnumType = enumType;
    ExtensionClass = extensionClass;
  }

  ///<summary>An <see langword="enum" /> type that has been extended by EnumEnhancer source generators.</summary>
  public Type EnumType { get; }

  /// <summary>A class containing extension methods for <see cref="EnumType" />.</summary>
  public Type ExtensionClass { get; }

  /// <inheritdoc />
  public override string ToString()
  {
    return $"{EnumType.Name},{ExtensionClass.Name}";
  }
}
