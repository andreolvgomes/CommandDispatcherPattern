using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace CommandDispatcher.Pattern.Interfaces
{
    public interface IQuerier
    {
        TResult Query<TResult>(IQuery<TResult> _query, object[] params_constructor = null, DbTransaction transaction = null);
    }
}
