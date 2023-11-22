using System;
using System.Diagnostics;
using SystemModeling.Network.Factories;
using SystemModeling.Network.Selectors;
using SystemModeling.Network.Statistics;
using SystemModeling.Network.TimeProviders;

namespace SystemModeling.Network;

public class CreateNode<T>(
        IJobFactory<T> factory,
        IProcessingTimeProvider<T> timeProvider,
        INetworkNodeSelector<T> nodeSelector,
        IStatisticsPolicy<T> statisticsPolicy,
        float completionTime = 0) 
    : NetworkNode<T>(statisticsPolicy)
{
    private float _completionTime = completionTime;

    private T _nextItem = factory.Create();

    public override float GetCompletionTime() => _completionTime;

    public override void Enter(T item) => throw new NotSupportedException();

    public override void Exit()
    {
        base.Exit();

        T prev = _nextItem;

        _nextItem = factory.Create();
        _completionTime = _currentTime + timeProvider.GetProcessingTime(_nextItem);

        var nextNode = nodeSelector.GetNext(ref prev);
        nextNode.Enter(prev);
    }

    public override void DebugPrint(bool verbose = false)
    {
        base.DebugPrint(verbose);
        Debug.WriteLine($"Created items: {ProcessedCount}");
    }
}
