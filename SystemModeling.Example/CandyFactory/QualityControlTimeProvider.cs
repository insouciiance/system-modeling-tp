using System;
using System.Collections.Generic;
using SystemModeling.Example.CandyFactory.Candies;
using SystemModeling.Network.TimeProviders;

namespace SystemModeling.Example.CandyFactory;

internal sealed class QualityControlTimeProvider : IProcessingTimeProvider<ICandy>
{
    private readonly Dictionary<Type, IProcessingTimeProvider<ICandy>> _timeProviders = new()
    {
        { typeof(ChocolateCandy), new ExponentialTimeProvider<ICandy>(0.15f) },
        { typeof(CaramelCandy), new ExponentialTimeProvider<ICandy>(0.1f) },
        { typeof(GummyCandy), new ExponentialTimeProvider<ICandy>(0.05f) }
    };

    public float GetProcessingTime(ICandy item) => _timeProviders[item.GetType()].GetProcessingTime(item);
}
