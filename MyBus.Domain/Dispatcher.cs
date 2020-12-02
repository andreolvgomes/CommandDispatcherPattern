using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using CommandDispatcher.Pattern;

namespace CommandDispatcher
{
    public class Dispatcher : IDispatcher
    {
        private readonly IDispatcherInvoke _dispatcher;

        /// <summary>
        /// Dispatcher
        /// </summary>
        /// <param name="dispatcher"></param>
        public Dispatcher(IDispatcherInvoke dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Execute something inherits of the IEvent
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="_event"></param>
        /// <returns></returns>
        public TResult Event<TResult>(IEvent<TResult> _event)
        {
            return _dispatcher.Event<TResult>(_event);
        }

        /// <summary>
        /// Execute something inherits of the IEvent
        /// </summary>
        /// <param name="_event"></param>
        public void Event(IEvent _event)
        {
            _dispatcher.Event(_event);
        }

        /// <summary>
        /// Execute something inherits of the ICommand
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="command"></param>
        /// <param name="params_constructor"></param>
        /// <returns></returns>
        public TResult Command<TResult>(ICommand<TResult> command, object[] params_constructor = null, SqlTransaction transaction = null)
        {
            return _dispatcher.Command<TResult>(command, params_constructor: params_constructor, transaction: transaction);
        }

        /// <summary>
        /// Execute something inherits of the ICommand
        /// </summary>
        /// <param name="command"></param>
        /// <param name="params_constructor"></param>
        public void Command(ICommand command, object[] params_constructor = null, SqlTransaction transaction = null)
        {
            _dispatcher.Command(command, params_constructor: params_constructor, transaction: transaction);
        }

        /// <summary>
        /// Execute something inherits of the IQuery
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="params_constructor"></param>
        /// <returns></returns>
        public TResult Query<TResult>(IQuery<TResult> query, object[] params_constructor = null, SqlTransaction transaction = null)
        {
            return _dispatcher.Query<TResult>(query, params_constructor: params_constructor, transaction: transaction);
        }

        /// <summary>
        /// Execute something inherits of the IFunction
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <param name="params_constructor"></param>
        /// <returns></returns>
        public TResult Function<TResult>(IFunction<TResult> function, object[] params_constructor = null, SqlTransaction transaction = null)
        {
            return _dispatcher.Function<TResult>(function, params_constructor: params_constructor, transaction: transaction);
        }

        /// <summary>
        /// Execute something inherits of the IFunction
        /// </summary>
        /// <param name="function"></param>
        /// <param name="params_constructor"></param>
        public void Function(IFunction function, object[] params_constructor = null, SqlTransaction transaction = null)
        {
            _dispatcher.Function(function, params_constructor: params_constructor, transaction: transaction);
        }
    }
}