using System;

namespace CommandDispatcher
{
    public interface IFunction : IFunction<Result> { }

    public interface IFunction<out TResult> : IBaseFunction { }

    public interface IBaseFunction { }
}