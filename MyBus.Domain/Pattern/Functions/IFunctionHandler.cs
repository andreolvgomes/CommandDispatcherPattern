using System;
using System.Data.Common;

namespace CommandDispatcher
{
    public interface IFunctionHandler<in TFunction> : IFunctionHandler<TFunction, Result>
        where TFunction : IFunction
    {
    }

    public interface IFunctionHandler<in TFunction, TResult>
        where TFunction : IFunction<TResult>
    {
        TResult Handle(TFunction function, DbTransaction transaction = null);
    }
}