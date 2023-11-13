namespace SystemModeling.Network.TimeProviders;

public class ConstantTimeProvider<T>(float processingTime) : IProcessingTimeProvider<T>
{
    public float GetProcessingTime(T _) => processingTime;
}
