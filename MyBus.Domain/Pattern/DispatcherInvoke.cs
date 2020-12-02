namespace CommandDispatcher.Pattern
{
    public class DispatcherInvoke : IDispatcherInvoke
    {
        private readonly IServiceContainer _serviceContainer;

        /// <summary>
        /// DispatcherInvoke
        /// </summary>
        /// <param name="serviceContainer"></param>
        public DispatcherInvoke(IServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        /// <summary>
        /// Execute something inherits of the IEvent
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="_event"></param>
        /// <returns></returns>
        public TResult Event<TResult>(IEvent<TResult> _event)
        {
            var handlerType = (typeof(IEventHandler<,>).MakeGenericType(_event.GetType(), typeof(TResult)));
            dynamic handler = _serviceContainer.GetInstance(handlerType);
            return handler.Handle((dynamic)_event);
        }

        /// <summary>
        /// Execute something inherits of the IEvent
        /// </summary>
        /// <param name="_event"></param>
        public void Event(IEvent _event)
        {
            var handlerType = typeof(IEventHandler<>).MakeGenericType(_event.GetType());
            dynamic handler = _serviceContainer.GetInstance(handlerType);
            handler.Handle((dynamic)_event);
        }

        /// <summary>
        /// Execute something inherits of the ICommand
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="command"></param>
        /// <param name="params_constructor"></param>
        /// <returns></returns>
        public TResult Command<TResult>(ICommand<TResult> command, object[] params_constructor = null)
        {
            var handlerType = (typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult)));
            dynamic handler = _serviceContainer.GetInstance(handlerType, params_constructor);
            return handler.Handle((dynamic)command);
        }

        /// <summary>
        /// Execute something inherits of the ICommand
        /// </summary>
        /// <param name="command"></param>
        /// <param name="params_constructor"></param>
        public void Command(ICommand command, object[] params_constructor = null)
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = _serviceContainer.GetInstance(handlerType, params_constructor);
            handler.Handle((dynamic)command);
        }

        /// <summary>
        /// Execute something inherits of the IQuery
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="_query"></param>
        /// <param name="params_constructor"></param>
        /// <returns></returns>
        public TResult Query<TResult>(IQuery<TResult> _query, object[] params_constructor = null)
        {
            var handlerType = (typeof(IQueryHandler<,>).MakeGenericType(_query.GetType(), typeof(TResult)));
            dynamic handler = _serviceContainer.GetInstance(handlerType, params_constructor);
            return handler.Handle((dynamic)_query);
        }

        /// <summary>
        /// Execute something inherits of the IFunction
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <param name="params_constructor"></param>
        /// <returns></returns>
        public TResult Function<TResult>(IFunction<TResult> function, object[] params_constructor = null)
        {
            var handlerType = (typeof(IFunctionHandler<,>).MakeGenericType(function.GetType(), typeof(TResult)));
            dynamic handler = _serviceContainer.GetInstance(handlerType, params_constructor);
            return handler.Handle((dynamic)function);
        }

        /// <summary>
        /// Execute something inherits of the IFunction
        /// </summary>
        /// <param name="function"></param>
        /// <param name="params_constructor"></param>
        public void Function(IFunction function, object[] params_constructor = null)
        {
            var handlerType = (typeof(IFunctionHandler<,>).MakeGenericType(function.GetType()));
            dynamic handler = _serviceContainer.GetInstance(handlerType, params_constructor);
            handler.Handle(function);
        }
    }
}