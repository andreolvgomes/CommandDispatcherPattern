using My.Tests.Events;
using MessagerBus;

namespace MyBus.Tests.Events
{
    public class EventsHandlers : IEventHandler<CreateNewEvent>,
        IEventHandler<DesmembradoProdutoEvent>
    {
        public Result Handle(DesmembradoProdutoEvent _event)
        {
            return Result.Default;
        }

        public Result Handle(CreateNewEvent _event)
        {
            return Result.Default;
        }
    }
}