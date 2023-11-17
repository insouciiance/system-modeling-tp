using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SystemModeling.Network.Processors;

public class PooledNodeProcessor<T>(IEnumerable<INetworkNodeProcessor<T>> nodes) : INetworkNodeProcessor<T>
{
    private readonly List<INetworkNodeProcessor<T>> _active = [];

    private float _currentTime;

    public T? Current => GetCurrent();

    public float CompletionTime => GetCompletionTime();

    public virtual bool TryEnter(T item)
    {
        foreach (var node in nodes)
        {
            node.CurrentTimeUpdated(_currentTime);

            if (node.TryEnter(item))
            {
                _active.Add(node);
                return true;
            }
        }

        return false;
    }

    public virtual bool TryExit()
    {
        for (int i = 0; i < _active.Count; i++)
        {
            var current = _active[i];

            if (Math.Abs(current.CompletionTime - CompletionTime) < 0.0001f)
            {
                _active.RemoveAt(i);
                return current.TryExit();
            }
        }

        throw new UnreachableException();
    }

    public void CurrentTimeUpdated(float currentTime)
    {
        _currentTime = currentTime;

        foreach (var node in _active)
            node.CurrentTimeUpdated(_currentTime);
    }

    private float GetCompletionTime()
    {
        if (_active.Count == 0)
            return float.PositiveInfinity;

        return _active.Min(n => n.CompletionTime);
    }

    private T? GetCurrent()
    {
        if (_active.Count == 0)
            return default;

        return _active.MinBy(n => n.CompletionTime)!.Current;
    }
}
