using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CommandDispatcher
{
    public interface IFunctionHandler<in TFunction> : IFunctionHandler<TFunction, Result>
        where TFunction : IFunction
    {
    }

    public interface IFunctionHandler<in TFunction, TResult>
        where TFunction : IFunction<TResult>
    {
        TResult Handle(TFunction function, SqlTransaction transaction = null);
    }
}