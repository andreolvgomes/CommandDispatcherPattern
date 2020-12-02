using CommandDispatcher;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

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

        public bool Handle(CreateNewCommand command, SqlTransaction transaction = null)
        {
            return true;
        }
    }
}