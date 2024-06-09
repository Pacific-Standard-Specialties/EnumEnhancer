namespace PSS.EnumEnhancers.ExtensionMethods.Attributes;

/// <summary>
///   Attribute written by the source generator for <see langword="enum" /> extension classes, for easier analysis and
///   reflection.
/// </summary>
/// <remarks>
///   Properties are just convenient shortcuts to properties of <typeparamref name="TEnum" />.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
[PublicAPI]
public sealed class ExtensionsForEnumTypeAttribute<TEnum> : Attribute, IExtensionsForEnumTypeAttributes where TEnum : struct, Enum
{
  /// <summary>
  ///   The namespace-qualified name of <typeparamref name="TEnum" />.
  /// </summary>
  public string EnumFullName => EnumType.FullName!;

  /// <summary>
  ///   The unqualified name of <typeparamref name="TEnum" />.
  /// </summary>
  public string EnumName => EnumType.Name;

  /// <summary>
  ///   The namespace containing <typeparamref name="TEnum" />.
  /// </summary>
  public string EnumNamespace => EnumType.Namespace!;

  /// <summary>
  ///   The <see cref="System.Type" /> given by <see langword="typeof" />(<typeparamref name="TEnum" />).
  /// </summary>
  public Type EnumType => typeof(TEnum);
}
