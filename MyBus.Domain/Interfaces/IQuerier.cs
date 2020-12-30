using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace CommandDispatcher.Interfaces
{
    public interface IQuerier
    {
        TResult Query<TResult>(IQuery<TResult> _query, object[] params_constructor = null, SqlTransaction transaction = null);
    }
}
