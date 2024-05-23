using EnumEnhancer.Attributes;

namespace EnumEnhancer.Debugging;

[GenerateEnumExtensionMethods]
public enum TestEnum
{
  Zero = 0,
  One,
  Two = 2,
  Three,
  Six = 6
}
