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

        public TResult Execute<TResult>(ICommand<TResult> _command)
        {
            return _dispatcher.Execute<TResult>(_command);
        }

        public void Execute(IFunction _command)
        {
            _dispatcher.Execute((ICommand<Result>)_command);
        }

        public TResult Query<TResult>(IQuery<TResult> _query)
        {
            return _dispatcher.Query<TResult>(_query);
        }
    }
}