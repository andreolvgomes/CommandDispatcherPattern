using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDispatcher.Pattern.Interfaces
{
    public interface IExecuter
    {
        TResult Function<TResult>(IFunction<TResult> function, params object[] params_constructor);
        void Function(IFunction function, params object[] params_constructor);
    }
}