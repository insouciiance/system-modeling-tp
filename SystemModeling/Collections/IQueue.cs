﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace SystemModeling.Collections;

public interface IQueue<T>
{
    int Count { get; }

    bool TryEnqueue(T item);

    bool TryDequeue([MaybeNullWhen(false)] out T item);

    bool TryPeek([MaybeNullWhen(false)] out T item);

    void DebugPrint() { }
}
