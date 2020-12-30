using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace CommandDispatcher.Interfaces
{
    public interface IExecuter
    {
        TResult Function<TResult>(IFunction<TResult> function, object[] params_constructor = null, SqlTransaction transaction = null);
        void Function(IFunction function, object[] params_constructor = null, SqlTransaction transaction = null);
    }
}