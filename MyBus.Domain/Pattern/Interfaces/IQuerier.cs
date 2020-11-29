using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDispatcher.Pattern.Interfaces
{
    public interface IQuerier
    {
        TResult Query<TResult>(IQuery<TResult> _query, params object[] params_constructor);
    }
}
