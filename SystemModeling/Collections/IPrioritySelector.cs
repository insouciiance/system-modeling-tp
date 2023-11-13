namespace SystemModeling.Collections;

public interface IPrioritySelector<T, TPriority>
{
    TPriority GetPriority(T item);
}
