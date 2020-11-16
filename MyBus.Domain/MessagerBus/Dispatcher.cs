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

        public TResult Command<TResult>(ICommand<TResult> command)
        {
            return _dispatcher.Command<TResult>(command);
        }

        public void Command(IFunction command)
        {
            _dispatcher.Command((ICommand<Result>)command);
        }

        public TResult Query<TResult>(IQuery<TResult> query)
        {
            return _dispatcher.Query<TResult>(query);
        }

        public TResult Function<TResult>(IFunction<TResult> function)
        {
            return _dispatcher.Function<TResult>(function);
        }

        public void Function(IFunction function)
        {
            _dispatcher.Function(function);
        }
    }
}