using System;
using System.Collections.Generic;
using SystemModeling.Example.CandyFactory.Candies;
using SystemModeling.Network.Processors;

namespace SystemModeling.Example.CandyFactory;

internal sealed class CaramelConveyorProcessor(IEnumerable<INetworkNodeProcessor<ICandy>> nodes) : PooledNodeProcessor<ICandy>(nodes)
{
    public override bool TryEnter(ICandy item)
    {
        if (base.TryEnter(item))
        {
            ((CaramelCandy)item).Aroma = Random.Shared.NextSingle() <= 0.6f ? CaramelAroma.Strawberry : CaramelAroma.Apricot;
            return true;
        }

        return false;
    }
}
