using System;
using System.Collections.Generic;
using SystemModeling.Example.CandyFactory.Candies;
using SystemModeling.Network.Processors;

namespace SystemModeling.Example.CandyFactory;

internal sealed class ChocolateConveyorProcessor(IEnumerable<INetworkNodeProcessor<ICandy>> nodes) : PooledNodeProcessor<ICandy>(nodes)
{
    public override bool TryEnter(ICandy item)
    {
        if (base.TryEnter(item))
        {
            ((ChocolateCandy)item).WithAdditives(GetAdditives());
            return true;
        }

        return false;
    }

    private static ChocolateAdditives GetAdditives()
    {
        ChocolateAdditives additives = ChocolateAdditives.None;

        if (Random.Shared.NextSingle() <= 0.15f)
            additives |= ChocolateAdditives.Cookies;

        if (Random.Shared.NextSingle() <= 0.25f)
            additives |= ChocolateAdditives.Nuts;

        if (Random.Shared.NextSingle() <= 0.3f)
            additives |= ChocolateAdditives.Raisins;

        return additives;
    }
}
