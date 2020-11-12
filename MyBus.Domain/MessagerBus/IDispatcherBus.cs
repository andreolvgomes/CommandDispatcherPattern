using MessagerBus;
using MessagerBus.Queries;

namespace MessagerBus
{
    public interface IDispatcherBus
    {
        TResult Event<TResult>(IEvent<TResult> _event);
        void Event(IEvent _event);

        TResult Execute<TResult>(ICommand<TResult> _command);
        void Execute(ICommand _command);

        TResult Query<TResult>(IQuery<TResult> _query);
    }
}