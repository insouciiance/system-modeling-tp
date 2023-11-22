using System;
using SystemModeling.Network;
using SystemModeling.Network.Statistics;

namespace SystemModeling.Extensions;

public static class StatisticsPolicyExtensions
{
    public static void RecordConditional<T>(this IStatisticsPolicy<T> policy, NetworkNode<T> node, float currentTime, Action callback)
    {
        if (policy.ShouldRecord(node, currentTime))
            callback.Invoke();
    }
}
