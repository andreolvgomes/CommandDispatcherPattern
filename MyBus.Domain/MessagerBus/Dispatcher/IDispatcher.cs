using MessagerBus.Interfaces;

namespace MessagerBus.Dispatcher
{
    public interface IDispatcher : IExecuter, IEventer, IQuerier
    {
    }
}