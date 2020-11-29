using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDispatcher.DispatcherPattern.Interfaces
{
    public interface IQuerier
    {
        TResult Query<TResult>(IQuery<TResult> _query, params object[] param_constructor);
    }
}
