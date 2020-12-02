using CommandDispatcher;
using CommandDispatcher.Pattern;

namespace CommandDispatcher
{
    public interface IDispatcher
    {
        TResult Event<TResult>(IEvent<TResult> _event);
        void Event(IEvent _event);

        TResult Command<TResult>(ICommand<TResult> command, object[] params_constructor = null);
        void Command(ICommand command, object[] params_constructor = null);

        TResult Function<TResult>(IFunction<TResult> _function, object[] params_constructor = null);
        void Function(IFunction function, object[] params_constructor = null);

        TResult Query<TResult>(IQuery<TResult> query, object[] params_constructor = null);
    }
}