// ReSharper disable All

using System;

namespace EnumEnhancer.Attributes;

/// <summary>
///     Interface to simplify general enumeration of constructed generic types for
///     <see cref="ExtensionsForEnumTypeAttribute{TEnum}"/>
/// </summary>
internal interface IExtensionsForEnumTypeAttributes
{
    Type EnumType { get; }
}
