using System;

namespace MessagerBus
{
    public interface IFunction : IFunction<Result> { }

    public interface IFunction<out TResult> : IBaseCommand { }

    public interface IBaseFunction { }
}