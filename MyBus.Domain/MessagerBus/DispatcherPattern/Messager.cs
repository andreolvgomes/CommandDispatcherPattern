using SimpleInjector;

namespace MessagerBus.DispatcherPattern
{
    public class Messager : IMessager
    {
        private readonly IServiceInstance _serviceInstance;

        /// <summary>
        /// Dispatcher
        /// </summary>
        /// <param name="serviceInstance"></param>
        public Messager(IServiceInstance serviceInstance)
        {
            _serviceInstance = serviceInstance;
        }

        /// <summary>
        /// Execute message event with return
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="_event"></param>
        /// <returns></returns>
        public TResult Event<TResult>(IEvent<TResult> _event)
        {
            var handlerType = (typeof(IEventHandler<,>).MakeGenericType(_event.GetType(), typeof(TResult)));
            dynamic handler = _serviceInstance.GetInstance(handlerType);
            return handler.Handle((dynamic)_event);
        }

        /// <summary>
        /// Execute message event
        /// </summary>
        /// <param name="_event"></param>
        public void Event(IEvent _event)
        {
            var handlerType = typeof(IEventHandler<>).MakeGenericType(_event.GetType());
            dynamic handler = _serviceInstance.GetInstance(handlerType);
            handler.Handle((dynamic)_event);
        }

        /// <summary>
        /// Execute message command with return
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public TResult Command<TResult>(ICommand<TResult> command)
        {
            var handlerType = (typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult)));
            dynamic handler = _serviceInstance.GetInstance(handlerType);
            return handler.Handle((dynamic)command);
        }

        /// <summary>
        /// Execute message command
        /// </summary>
        /// <param name="command"></param>
        public void Command(IFunction command)
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = _serviceInstance.GetInstance(handlerType);
            handler.Handle((dynamic)command);
        }

        /// <summary>
        /// Execute message query with return
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="_query"></param>
        /// <returns></returns>
        public TResult Query<TResult>(IQuery<TResult> _query)
        {
            var handlerType = (typeof(IQueryHandler<,>).MakeGenericType(_query.GetType(), typeof(TResult)));
            dynamic handler = _serviceInstance.GetInstance(handlerType);
            return handler.Handle((dynamic)_query);
        }

        /// <summary>
        /// Execute message function with return
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public TResult Function<TResult>(IFunction<TResult> function)
        {
            var handlerType = (typeof(IFunctionHandler<,>).MakeGenericType(function.GetType(), typeof(TResult)));
            dynamic handler = _serviceInstance.GetInstance(handlerType);
            return handler.Handle((dynamic)function);
        }

        /// <summary>
        /// Execute message function
        /// </summary>
        /// <param name="function"></param>
        public void Function(IFunction function)
        {
            var handlerType = (typeof(IFunctionHandler<,>).MakeGenericType(function.GetType()));
            dynamic handler = _serviceInstance.GetInstance(handlerType);
            handler.Handle(function);
        }
    }
}