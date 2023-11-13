﻿namespace SystemModeling.Network.TimeProviders;

public interface IProcessingTimeProvider<T>
{
    float GetProcessingTime(T item);
}
