using System;
using System.Collections.Generic;
using SystemModeling.Example.CandyFactory.Candies;
using SystemModeling.Network;
using SystemModeling.Network.Selectors;

namespace SystemModeling.Example.CandyFactory;

internal sealed class QualityControlNodeSelector(DisposeNode<ICandy> finishedCandy, DisposeNode<ICandy> defectiveCandy) : INetworkNodeSelector<ICandy>
{
    private readonly Dictionary<Type, INetworkNodeSelector<ICandy>> _nodeSelectors = new()
    {
        { typeof(ChocolateCandy), new WeightedNodeSelector<ICandy>((finishedCandy, 0.95f), (defectiveCandy, 0.05f)) },
        { typeof(CaramelCandy), new WeightedNodeSelector<ICandy>((finishedCandy, 0.96f), (defectiveCandy, 0.04f)) },
        { typeof(GummyCandy), new WeightedNodeSelector<ICandy>((finishedCandy, 0.99f), (defectiveCandy, 0.01f)) }
    };

    public NetworkNode<ICandy> GetNext(ref ICandy item) => _nodeSelectors[item.GetType()].GetNext(ref item);
}
