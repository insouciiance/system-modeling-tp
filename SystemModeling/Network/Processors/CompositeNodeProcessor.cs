using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace SystemModeling.Network.Processors;

public class CompositeNodeProcessor<T>(ImmutableArray<INetworkNodeProcessor<T>> nodes) : INetworkNodeProcessor<T>
{
    public T? Current => nodes.MinBy(n => n.CompletionTime)!.Current;

    public float CompletionTime => nodes.Min(n => n.CompletionTime);

    public CompositeNodeProcessor(params INetworkNodeProcessor<T>[] nodes)
        : this(nodes.ToImmutableArray()) { }

    public bool TryEnter(T item)
    {
        foreach (var node in nodes)
        {
            if (node.TryEnter(item))
                return true;
        }

        return false;
    }

    public bool TryExit()
    {
        foreach (var node in nodes)
        {
            if (Math.Abs(node.CompletionTime - CompletionTime) < 0.0001f)
                return node.TryExit();
        }

        throw new UnreachableException();
    }

    public void CurrentTimeUpdated(float currentTime)
    {
        foreach (var node in nodes)
            node.CurrentTimeUpdated(currentTime);
    }
}
