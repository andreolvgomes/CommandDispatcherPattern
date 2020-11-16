using MessagerBus;
using MessagerBus.DispatcherPattern;

namespace MessagerBus
{
    public interface IDispatcher
    {
        TResult Event<TResult>(IEvent<TResult> _event);
        void Event(IEvent _event);

        TResult Command<TResult>(ICommand<TResult> _command);
        void Command(IFunction _command);

        TResult Function<TResult>(IFunction<TResult> _function);
        void Function(IFunction _function);

        TResult Query<TResult>(IQuery<TResult> _query);
    }
}