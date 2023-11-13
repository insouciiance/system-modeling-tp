namespace SystemModeling.Collections;

public sealed class LimitedWorkStealingQueue<T>(int maxSize, int sizeDelta) : WorkStealingQueue<T>(sizeDelta)
{
    public override bool TryEnqueue(T item) => Count < maxSize && base.TryEnqueue(item);
}
