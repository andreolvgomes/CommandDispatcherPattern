using CommandDispatcher;

namespace MyBus.Tests.Commands
{
    public class CommandsHandlers
        : ICommandHandler<CreateNewCommand, bool>
    {
        private readonly IDispatcher _dispatcher;

        public CommandsHandlers(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public bool Handle(CreateNewCommand command)
        {
            return true;
        }
    }
}