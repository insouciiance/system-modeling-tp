using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace SystemModeling.Network;

public class NetworkModel<T>(ImmutableArray<NetworkNode<T>> nodes)
{
    private float _currentTime;

    public Action? DebugPrintExtra { get; set; }

    public NetworkModel(params NetworkNode<T>[] nodes)
        : this(nodes.ToImmutableArray())
    { }

    public void Simulate(float simulationTime)
    {
        while (_currentTime < simulationTime)
        {
            var nextNode = nodes.MinBy(n => n.GetCompletionTime())!;
            _currentTime = nextNode.GetCompletionTime();

            Debug.WriteLine("\n*************** Current Time Update ***************");
            Debug.WriteLine($"Updated from {nextNode.DebugName}");
            Debug.WriteLine($"Current time: {_currentTime}");

            foreach (var node in nodes)
            {
                node.CurrentTimeUpdated(_currentTime);

                Debug.WriteLine("\n");
                node.DebugPrint();
            }

            foreach (var node in nodes)
            {
                if (Math.Abs(node.GetCompletionTime() - _currentTime) < .0001f)
                {
                    Debug.WriteLine($"Calling {node.DebugName} Exit");
                    node.Exit();
                }
            }
        }

        Debug.WriteLine("\n*************** Simulation Finished ***************");

        foreach (var node in nodes)
        {
            Debug.WriteLine("\n");
            node.DebugPrint(true);
        }

        Debug.WriteLine("\n");
        DebugPrintExtra?.Invoke();
    }
}
