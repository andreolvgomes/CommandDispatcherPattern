using My.Tests.Events;
using MessagerBus;
using System;

namespace MyBus.Tests.Events
{
    public class EventsHandlers : IEventHandler<CreateNewEvent>
    {
        public Result Handle(CreateNewEvent _event)
        {
            return Result.Default;
        }
    }
}