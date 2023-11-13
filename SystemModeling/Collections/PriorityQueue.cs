using System.Diagnostics.CodeAnalysis;
using BCL = System.Collections.Generic;

namespace SystemModeling.Collections;

public class PriorityQueue<TItem, TPriority>(IPrioritySelector<TItem, TPriority> prioritySelector) : IQueue<TItem>
{
    private readonly BCL.PriorityQueue<TItem, TPriority> _queue = new();

    public int Count => _queue.Count;

    public virtual bool TryEnqueue(TItem item)
    {
        var priority = prioritySelector.GetPriority(item);
        _queue.Enqueue(item, priority);
        return true;
    }

    public virtual bool TryDequeue([MaybeNullWhen(false)] out TItem item) => _queue.TryDequeue(out item, out _);

    public virtual bool TryPeek([MaybeNullWhen(false)] out TItem item) => _queue.TryPeek(out item, out _);
}
