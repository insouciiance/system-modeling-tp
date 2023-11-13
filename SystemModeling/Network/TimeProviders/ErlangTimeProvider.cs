using System;
using MathNet.Numerics.Distributions;

namespace SystemModeling.Network.TimeProviders;

public class ErlangTimeProvider<T>(int k, float mean) : IProcessingTimeProvider<T>
{
    private readonly Erlang _erlang = new(k, mean, Random.Shared);

    public float GetProcessingTime(T _) => (float)_erlang.Sample();
}
