using System;

namespace SystemModeling.Network.TimeProviders;

public class ExponentialTimeProvider<T>(float mean) : IProcessingTimeProvider<T>
{
    public float GetProcessingTime(T _)
    {
        float rnd = Random.Shared.NextSingle();

        if (rnd == 0)
            rnd = float.Epsilon;

        return -mean * MathF.Log(rnd);
    }
}
