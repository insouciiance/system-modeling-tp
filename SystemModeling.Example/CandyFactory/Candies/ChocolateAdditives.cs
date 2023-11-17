using System;

namespace SystemModeling.Example.CandyFactory.Candies;

[Flags]
internal enum ChocolateAdditives
{
    None = 0,
    Cookies = 1 << 0,
    Nuts = 1 << 1,
    Raisins = 1 << 2
}
