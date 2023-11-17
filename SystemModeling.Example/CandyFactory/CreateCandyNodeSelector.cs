using System.Collections.Generic;
using System;
using SystemModeling.Example.CandyFactory.Candies;
using SystemModeling.Network;
using SystemModeling.Network.Selectors;

namespace SystemModeling.Example.CandyFactory;

internal sealed class CreateCandyNodeSelector(
    ProcessNode<ICandy> processChocolate,
    ProcessNode<ICandy> processCaramel,
    ProcessNode<ICandy> processGummy)
    : INetworkNodeSelector<ICandy>
{
    private readonly Dictionary<Type, INetworkNodeSelector<ICandy>> _nodeSelectors = new()
    {
        { typeof(ChocolateCandy), new ConstantNodeSelector<ICandy>(processChocolate) },
        { typeof(CaramelCandy), new ConstantNodeSelector<ICandy>(processCaramel) },
        { typeof(GummyCandy), new ConstantNodeSelector<ICandy>(processGummy) }
    };

    public NetworkNode<ICandy> GetNext(ref ICandy item) => _nodeSelectors[item.GetType()].GetNext(ref item);
}
