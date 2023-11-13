using System;

namespace SystemModeling.Network.TimeProviders;

public class UniformTimeProvider<T>(float min, float max) : IProcessingTimeProvider<T>
{
    public float GetProcessingTime(T _) => min + Random.Shared.NextSingle() * (max - min);
}
