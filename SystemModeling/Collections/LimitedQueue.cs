namespace SystemModeling.Collections;

public sealed class LimitedQueue<T>(int maxSize) : Queue<T>
{
    public override bool TryEnqueue(T item) => Count < maxSize && base.TryEnqueue(item);
}
