namespace SystemModeling.Network.Statistics;

public class DefaultStatisticsPolicy<T> : IStatisticsPolicy<T>
{
    public bool ShouldRecord(NetworkNode<T> node, float currentTime) => true;
}
