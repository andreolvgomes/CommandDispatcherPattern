using System;
using CommandDispatcher.Pattern;

namespace CommandDispatcher
{
    public class Dispatcher : IDispatcher
    {
        private readonly IDispatcherInvoke _dispatcher;

        public Dispatcher(IDispatcherInvoke dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public TResult Event<TResult>(IEvent<TResult> _event)
        {
            return _dispatcher.Event<TResult>(_event);
        }

        public void Event(IEvent _event)
        {
            _dispatcher.Event(_event);
        }

        public TResult Command<TResult>(ICommand<TResult> command, params object[] params_constructor)
        {
            return _dispatcher.Command<TResult>(command, params_constructor);
        }

        public void Command(ICommand command, params object[] params_constructor)
        {
            _dispatcher.Command(command, params_constructor);
        }

        public TResult Query<TResult>(IQuery<TResult> query, params object[] params_constructor)
        {
            return _dispatcher.Query<TResult>(query, params_constructor);
        }

        public TResult Function<TResult>(IFunction<TResult> function, params object[] params_constructor)
        {
            return _dispatcher.Function<TResult>(function, params_constructor);
        }

        public void Function(IFunction function, params object[] params_constructor)
        {
            _dispatcher.Function(function, params_constructor);
        }
    }
}