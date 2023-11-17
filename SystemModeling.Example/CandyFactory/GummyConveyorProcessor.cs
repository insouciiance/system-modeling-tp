using System;
using System.Collections.Generic;
using SystemModeling.Example.CandyFactory.Candies;
using SystemModeling.Network.Processors;

namespace SystemModeling.Example.CandyFactory;

internal sealed class GummyConveyorProcessor(IEnumerable<INetworkNodeProcessor<ICandy>> nodes) : PooledNodeProcessor<ICandy>(nodes)
{
    public override bool TryEnter(ICandy item)
    {
        if (base.TryEnter(item))
        {
            ((GummyCandy)item).WithAdditives(GetAdditives());
            return true;
        }

        return false;
    }

    private static GummyAdditives GetAdditives()
    {
        GummyAdditives additives = GummyAdditives.None;

        if (Random.Shared.NextSingle() <= 0.5f)
            additives |= GummyAdditives.Sugar;

        if (Random.Shared.NextSingle() <= 0.3f)
            additives |= GummyAdditives.Sour;

        return additives;
    }
}
