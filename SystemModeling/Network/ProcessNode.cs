using System;
using System.Diagnostics;
using System.Text;
using SystemModeling.Collections;
using SystemModeling.Network.Processors;
using SystemModeling.Network.Selectors;

namespace SystemModeling.Network;

public class ProcessNode<T>(INetworkNodeProcessor<T> nodeProcessor, IQueue<T> queue) : NetworkNode<T>
{
    public IQueue<T> Queue { get; } = queue;

    public INetworkNodeSelector<T>? NextNodeSelector { get; set; }

    public float QueueAverageSize => QueueWaitingTimeTotal / _currentTime;

    public float QueueWaitingTimeTotal { get; private set; }

    public int FailuresCount { get; private set; }

    public override float GetCompletionTime() => nodeProcessor.CompletionTime;

    public override void Enter(T item)
    {
        base.Enter(item);

        if (nodeProcessor.TryEnter(item))
        {
            Debug.WriteLine($"{DebugName} Started processing");
            return;
        }

        if (Queue.TryEnqueue(item))
        {
            Debug.WriteLine($"{DebugName} Queued an item");
            return;
        }

        Debug.WriteLine($"{DebugName} Failure!");
        FailuresCount++;
    }

    public override void Exit()
    {
        base.Exit();

        var current = nodeProcessor.Current;
        nodeProcessor.TryExit();

        if (NextNodeSelector is not null)
        {
            Debug.Assert(current is not null);

            var nextNode = NextNodeSelector.GetNext(ref current);
            Debug.WriteLine($"{DebugName} -> {nextNode.DebugName}");
            nextNode.Enter(current);
        }

        if (Queue.TryPeek(out var item) && nodeProcessor.TryEnter(item))
        {
            Debug.Assert(Queue.TryDequeue(out _));

            Debug.WriteLine($"{DebugName} Queue not empty, new item processing");
        }
    }

    public override void CurrentTimeUpdated(float currentTime)
    {
        nodeProcessor.CurrentTimeUpdated(currentTime);

        float delta = currentTime - _currentTime;

        QueueWaitingTimeTotal += delta * Queue.Count;

        base.CurrentTimeUpdated(currentTime);
    }

    public override void DebugPrint(bool verbose = false)
    {
        base.DebugPrint(verbose);

        StringBuilder prettyQueue = new();
        prettyQueue.Append(new string('*', Queue.Count));

        Debug.WriteLine($"Queue size: {Queue.Count} ({prettyQueue})");
        Debug.WriteLine($"Failures: {FailuresCount}");

        if (verbose)
        {
            Debug.WriteLine($"Processed items: {ProcessedCount}");
            Debug.WriteLine($"Average queue size: {QueueAverageSize}");
            Debug.WriteLine($"Failure probability: {(float)FailuresCount / (FailuresCount + ProcessedCount)}");

            Queue.DebugPrint();
            nodeProcessor.DebugPrint();
        }
    }
}
