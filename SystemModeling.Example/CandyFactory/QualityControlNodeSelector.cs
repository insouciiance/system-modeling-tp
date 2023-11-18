using System;
using SystemModeling.Example.CandyFactory.Candies;
using SystemModeling.Example.Extensions;
using SystemModeling.Network;
using SystemModeling.Network.Selectors;

namespace SystemModeling.Example.CandyFactory;

internal sealed class QualityControlNodeSelector(DisposeNode<ICandy> finishedCandy, DisposeNode<ICandy> defectiveCandy) : INetworkNodeSelector<ICandy>
{
    public NetworkNode<ICandy> GetNext(ref ICandy item) => item switch
    {
        ChocolateCandy x => GetNodeForChocolateCandy(x),
        CaramelCandy x => GetNodeForCaramelCandy(x),
        GummyCandy x => GetNodeForGummyCandy(x),
        _ => throw new NotSupportedException()
    };

    private NetworkNode<ICandy> GetNodeForChocolateCandy(ChocolateCandy candy)
    {
        float threshold = 0.05f + 0.01f * ((int)candy.Additives).SetBitsCount();
        return Random.Shared.NextSingle() <= threshold ? defectiveCandy : finishedCandy;
    }

    private NetworkNode<ICandy> GetNodeForCaramelCandy(CaramelCandy candy)
    {
        float threshold = candy.Aroma switch
        {
            CaramelAroma.Strawberry => 0.04f,
            CaramelAroma.Apricot => 0.045f,
            _ => throw new NotSupportedException()
        };

        return Random.Shared.NextSingle() <= threshold ? defectiveCandy : finishedCandy;
    }

    private NetworkNode<ICandy> GetNodeForGummyCandy(GummyCandy candy)
    {
        float threshold = 0.05f + 0.01f * ((int)candy.Additives).SetBitsCount();
        return Random.Shared.NextSingle() <= threshold ? defectiveCandy : finishedCandy;
    }
}
