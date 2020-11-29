using System;

namespace CommandDispatcher
{
    public interface IFunctionHandler<in TFunction> : IFunctionHandler<TFunction, Result>
        where TFunction : IFunction
    {
    }

    public interface IFunctionHandler<in TFunction, TResult>
        where TFunction : IFunction<TResult>
    {
        TResult Handle(TFunction function);
    }
}