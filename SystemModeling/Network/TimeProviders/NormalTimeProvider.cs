using System;
using SystemModeling.Extensions;

namespace SystemModeling.Network.TimeProviders;

public class NormalTimeProvider<T>(float mean, float stdDev) : IProcessingTimeProvider<T>
{
    public float GetProcessingTime(T _) => Random.Shared.NextGaussian() * stdDev + mean;
}
