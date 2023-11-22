using System;
using System.Diagnostics;
using SystemModeling.Extensions;
using SystemModeling.Network.Statistics;

namespace SystemModeling.Network;

public class DisposeNode<T>(IStatisticsPolicy<T> statisticsPolicy) : NetworkNode<T>(statisticsPolicy)
{
    private float _deltaTimeSum;

    private float _previousEnterTime;

    public float AverageDeltaTime => _deltaTimeSum / ProcessedCount;

    public override float GetCompletionTime() => float.PositiveInfinity;

    public override void Enter(T item)
    {
        base.Enter(item);
        _deltaTimeSum += _currentTime - _previousEnterTime;
        _previousEnterTime = _currentTime;

        statisticsPolicy.RecordConditional(this, _currentTime, () => ProcessedCount++);
    }

    public override void Exit() => throw new InvalidOperationException();

    public override void DebugPrint(bool verbose = false)
    {
        base.DebugPrint(verbose);

        if (verbose)
        {
            Debug.WriteLine($"Average delta time: {AverageDeltaTime}");
            Debug.WriteLine($"Processed items: {ProcessedCount}");
        }      
    }
}
