namespace SystemModeling.Network.Selectors;

public class ConstantNodeSelector<T>(NetworkNode<T> node) : INetworkNodeSelector<T>
{
    public NetworkNode<T> GetNext(ref T _) => node;
}
