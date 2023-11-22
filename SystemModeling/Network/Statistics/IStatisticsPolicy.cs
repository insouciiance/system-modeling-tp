namespace SystemModeling.Network.Statistics;

public interface IStatisticsPolicy<T>
{
    bool ShouldRecord(NetworkNode<T> node, float currentTime);
}
