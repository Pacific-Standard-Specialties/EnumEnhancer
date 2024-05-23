
namespace EnumEnhancer.Debugging;

public static class Program
{
    public static void Main ()
    {
      TestEnum e = TestEnum.One;
      Console.WriteLine (e.AsInt32());
      Console.WriteLine (e.AsUInt32());
      Console.WriteLine (e.FastHasFlags(TestEnum.Zero));
      Console.WriteLine (e.FastHasFlags(TestEnum.One));
      Console.WriteLine (e.FastHasFlags(0));
      Console.WriteLine (e.FastHasFlags(1));
      Console.WriteLine (e.FastIsDefined(0));
      Console.WriteLine (e.FastIsDefined(1));
      Console.WriteLine (e.FastIsDefined(20));
    }
}