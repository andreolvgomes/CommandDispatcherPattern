using System;
using System.Collections.Generic;
using System.Text;

namespace MessagerBus.DispatcherPattern.Interfaces
{
    public interface IExecuter
    {
        TResult Function<TResult>(IFunction<TResult> function);
        void Function(IFunction function);
    }
}