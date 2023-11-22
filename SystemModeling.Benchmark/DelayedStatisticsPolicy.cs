using SystemModeling.Network;
using SystemModeling.Network.Statistics;

namespace SystemModeling.Benchmark;

internal class DelayedStatisticsPolicy<T>(float delay) : IStatisticsPolicy<T>
{
    public bool ShouldRecord(NetworkNode<T> node, float currentTime) => currentTime > delay;
}
