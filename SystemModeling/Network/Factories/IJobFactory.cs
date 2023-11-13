namespace SystemModeling.Network.Factories;

public interface IJobFactory<T>
{
    T Create();
}
