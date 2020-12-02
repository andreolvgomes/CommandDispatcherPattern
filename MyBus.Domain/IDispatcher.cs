using CommandDispatcher;
using CommandDispatcher.Pattern;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CommandDispatcher
{
    public interface IDispatcher
    {
        TResult Event<TResult>(IEvent<TResult> _event);
        void Event(IEvent _event);

        TResult Command<TResult>(ICommand<TResult> command, object[] params_constructor = null, SqlTransaction transaction = null);
        void Command(ICommand command, object[] params_constructor = null, SqlTransaction transaction = null);

        TResult Function<TResult>(IFunction<TResult> _function, object[] params_constructor = null, SqlTransaction transaction = null);
        void Function(IFunction function, object[] params_constructor = null, SqlTransaction transaction = null);

        TResult Query<TResult>(IQuery<TResult> query, object[] params_constructor = null, SqlTransaction transaction = null);
    }
}