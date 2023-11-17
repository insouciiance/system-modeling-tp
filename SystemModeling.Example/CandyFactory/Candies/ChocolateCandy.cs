using SystemModeling.Example.Extensions;

namespace SystemModeling.Example.CandyFactory.Candies;

internal class ChocolateCandy : ICandy
{
    public ChocolateAdditives Additives { get; private set; }

    public void WithAdditives(ChocolateAdditives additives) => Additives |= additives;

    public float GetPrice() => 3 + ((int)Additives).SetBitsCount();
}
