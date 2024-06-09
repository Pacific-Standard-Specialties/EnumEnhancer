namespace PSS.EnumEnhancers.ExtensionMethods.Attributes;

/// <summary>
///   Interface to simplify general enumeration of constructed generic types for
///   <see cref="ExtensionsForEnumTypeAttribute{TEnum}" />
/// </summary>
[PublicAPI]
public interface IExtensionsForEnumTypeAttributes
{
  /// <summary>
  ///   Gets the <see cref="Type" /> of the <see langword="enum" /> for which this extension class was generated.
  /// </summary>
  Type EnumType { get; }
}
