﻿using MessagerBus.Interfaces;
using MessagerBus.Queries;
using SimpleInjector;

namespace MessagerBus.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        private readonly Container _container;

        /// <summary>
        /// Dispatcher
        /// </summary>
        /// <param name="container"></param>
        public Dispatcher(Container container)
        {
            _container = container;
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
            dynamic handler = _container.GetInstance(handlerType);
            return handler.Handle((dynamic)_event);
        }

        /// <summary>
        /// Execute message event
        /// </summary>
        /// <param name="_event"></param>
        public void Event(IEvent _event)
        {
            var handlerType = typeof(IEventHandler<>).MakeGenericType(_event.GetType());
            dynamic handler = _container.GetInstance(handlerType);
            handler.Handle((dynamic)_event);
        }

        /// <summary>
        /// Execute message command with return
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public TResult Execute<TResult>(ICommand<TResult> command)
        {
            var handlerType = (typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult)));
            dynamic handler = _container.GetInstance(handlerType);
            return handler.Handle((dynamic)command);
        }

        /// <summary>
        /// Execute message command
        /// </summary>
        /// <param name="command"></param>
        public void Execute(ICommand command)
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = _container.GetInstance(handlerType);
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
            dynamic handler = _container.GetInstance(handlerType);
            return handler.Handle((dynamic)_query);
        }
    }
}