using MessagerBus.Dispatcher;
using MessagerBus.Queries;

namespace MessagerBus
{
    public class DispatcherBus : IDispatcherBus
    {
        private readonly IDispatcher _dispatcher;

        public DispatcherBus(IDispatcher dispatcher)
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

        public void Execute(ICommand _command)
        {
            _dispatcher.Execute(_command);
        }

        public TResult Query<TResult>(IQuery<TResult> _query)
        {
            return _dispatcher.Query<TResult>(_query);
        }
    }
}