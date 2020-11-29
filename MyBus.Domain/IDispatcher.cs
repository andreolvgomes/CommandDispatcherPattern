using CommandDispatcher;
using CommandDispatcher.DispatcherPattern;

namespace CommandDispatcher
{
    public interface IDispatcher
    {
        TResult Event<TResult>(IEvent<TResult> _event);
        void Event(IEvent _event);

        TResult Command<TResult>(ICommand<TResult> command, params object[] param_constructor);
        void Command(ICommand command, params object[] param_constructor);

        TResult Function<TResult>(IFunction<TResult> _function, params object[] param_constructor);
        void Function(IFunction function, params object[] param_constructor);

        TResult Query<TResult>(IQuery<TResult> query, params object[] param_constructor);
    }
}