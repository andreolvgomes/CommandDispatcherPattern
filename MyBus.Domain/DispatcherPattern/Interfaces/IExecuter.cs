using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDispatcher.DispatcherPattern.Interfaces
{
    public interface IExecuter
    {
        TResult Function<TResult>(IFunction<TResult> function, params object[] param_constructor);
        void Function(IFunction function, params object[] param_constructor);
    }
}