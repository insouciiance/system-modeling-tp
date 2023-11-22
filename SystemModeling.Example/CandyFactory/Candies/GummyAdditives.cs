using System;

namespace SystemModeling.Example.CandyFactory.Candies;

[Flags]
public enum GummyAdditives
{
    None = 0,
    Sugar = 1 << 0,
    Sour = 1 << 1
}
