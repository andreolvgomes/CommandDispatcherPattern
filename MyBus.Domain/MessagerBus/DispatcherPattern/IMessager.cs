using MessagerBus.DispatcherPattern.Interfaces;

namespace MessagerBus.DispatcherPattern
{
    public interface IMessager : IExecuter, IEventer, IQuerier
    {
    }
}