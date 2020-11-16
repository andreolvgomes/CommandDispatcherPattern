using MessagerBus;
using MessagerBus.DispatcherPattern;

namespace MessagerBus
{
    public interface IDispatcher
    {
        TResult Event<TResult>(IEvent<TResult> _event);
        void Event(IEvent _event);

        TResult Execute<TResult>(ICommand<TResult> _command);
        void Execute(IFunction _command);

        TResult Query<TResult>(IQuery<TResult> _query);
    }
}