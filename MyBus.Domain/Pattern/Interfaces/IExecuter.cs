using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDispatcher.Pattern.Interfaces
{
    public interface IExecuter
    {
        TResult Function<TResult>(IFunction<TResult> function, object[] params_constructor = null);
        void Function(IFunction function, object[] params_constructor = null);
    }
}