using CommandDispatcher;
using CommandDispatcher.Pattern;

namespace CommandDispatcher
{
    public interface IDispatcher
    {
        TResult Event<TResult>(IEvent<TResult> _event);
        void Event(IEvent _event);

        TResult Command<TResult>(ICommand<TResult> command, params object[] params_constructor);
        void Command(ICommand command, params object[] params_constructor);

        TResult Function<TResult>(IFunction<TResult> _function, params object[] params_constructor);
        void Function(IFunction function, params object[] params_constructor);

        TResult Query<TResult>(IQuery<TResult> query, params object[] params_constructor);
    }
}