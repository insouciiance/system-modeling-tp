using System;
using System.Diagnostics.CodeAnalysis;

namespace SystemModeling.Collections;

public sealed class ThrowingQueue<T> : IQueue<T>
{
    public int Count => 0;

    public bool TryDequeue([MaybeNullWhen(false)] out T item) => throw new NotSupportedException();

    public bool TryEnqueue(T item) => throw new NotSupportedException();

    public bool TryPeek([MaybeNullWhen(false)] out T item)
    {
        item = default;
        return false;
    }
}
