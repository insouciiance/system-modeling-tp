using System;
using SystemModeling.Example.CandyFactory.Candies;
using SystemModeling.Network.Factories;

namespace SystemModeling.Example.CandyFactory;

internal class CandyFactory : IJobFactory<ICandy>
{
    public ICandy Create() => Random.Shared.NextSingle() switch
    {
        <= 0.4f => new ChocolateCandy(),
        <= 0.7f => new CaramelCandy(),
        _ => new GummyCandy()
    };
}
