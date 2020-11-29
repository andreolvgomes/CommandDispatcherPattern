using System;

namespace CommandDispatcher
{
    public interface IEvent : IEvent<Result> { }

    public interface IEvent<out TResult> : IBaseEvent { }

    public interface IBaseEvent { }
}