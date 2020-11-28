using System;
using MessagerBus.DispatcherPattern;

namespace MessagerBus
{
    public class Dispatcher : IDispatcher
    {
        private readonly IMessager _dispatcher;

        public Dispatcher(IMessager dispatcher)
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

        public TResult Command<TResult>(ICommand<TResult> command, params object[] param_constructor)
        {
            return _dispatcher.Command<TResult>(command, param_constructor);
        }

        public void Command(ICommand command, params object[] param_constructor)
        {
            _dispatcher.Command(command, param_constructor);
        }

        public TResult Query<TResult>(IQuery<TResult> query, params object[] param_constructor)
        {
            return _dispatcher.Query<TResult>(query, param_constructor);
        }

        public TResult Function<TResult>(IFunction<TResult> function, params object[] param_constructor)
        {
            return _dispatcher.Function<TResult>(function, param_constructor);
        }

        public void Function(IFunction function, params object[] param_constructor)
        {
            _dispatcher.Function(function, param_constructor);
        }
    }
}