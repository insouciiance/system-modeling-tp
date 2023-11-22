using System;
using System.Diagnostics;
using SystemModeling.Extensions;
using SystemModeling.Network.Statistics;

namespace SystemModeling.Network;

public abstract class NetworkNode<T>(IStatisticsPolicy<T> statisticsPolicy)
{
    protected float _currentTime;

    public int ProcessedCount { get; protected set; }

    public string? DebugName { get; init; }

    public event Action<T, float, IStatisticsPolicy<T>>? OnEnter;

    public event Action<float, IStatisticsPolicy<T>>? OnExit;

    public abstract float GetCompletionTime();

    public virtual void Enter(T item)
    {
        OnEnter?.Invoke(item, _currentTime, statisticsPolicy);
    }

    public virtual void Exit()
    {
        OnExit?.Invoke(_currentTime, statisticsPolicy);

        statisticsPolicy.RecordConditional(this, _currentTime, () => ProcessedCount++);
    }

    public virtual void CurrentTimeUpdated(float currentTime) => _currentTime = currentTime;

    [Conditional("DEBUG")]
    public virtual void DebugPrint(bool verbose = false)
    {
        Debug.WriteLine(DebugName);
    }
}
