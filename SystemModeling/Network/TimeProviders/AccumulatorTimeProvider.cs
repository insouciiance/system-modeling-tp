namespace SystemModeling.Network.TimeProviders;

public class AccumulatorTimeProvider<T>(IProcessingTimeProvider<T> wrappee) : IProcessingTimeProvider<T>
{
    public float TotalProcessingTime { get; private set; }

    public float GetProcessingTime(T item)
    {
        float time = wrappee.GetProcessingTime(item);
        TotalProcessingTime += time;
        return time;
    }
}
