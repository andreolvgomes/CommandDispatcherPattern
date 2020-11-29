using System;

namespace CommandDispatcher
{
    public interface ICommand : ICommand<Result> { }

    public interface ICommand<out TResult> : IBaseCommand { }

    public interface IBaseCommand { }
}