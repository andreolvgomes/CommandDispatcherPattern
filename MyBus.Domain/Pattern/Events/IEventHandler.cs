using System;

namespace CommandDispatcher
{
    public interface IEventHandler<in TEvent> : IEventHandler<TEvent, Result>
        where TEvent : IEvent
    {
    }

    public interface IEventHandler<in TEvent, TResult>
        where TEvent : IEvent<TResult>
    {
        TResult Handle(TEvent _event);
    }
}
