using SystemModeling.Example.Extensions;

namespace SystemModeling.Example.CandyFactory.Candies;

public class GummyCandy : ICandy
{
    public GummyAdditives Additives { get; private set; }

    public void WithAdditives(GummyAdditives additives) => Additives |= additives;

    public float GetPrice() => 3 + ((int)Additives).SetBitsCount();
}
