using My.Tests.Commands;
using My.Tests.Events;
using MessagerBus;

namespace MyBus.Tests.Commands
{
    public class CommandHandlers
        : ICommandHandler<CreateNewCommand, bool>,
        ICommandHandler<DeleteCommand>,
        ICommandHandler<DesmebrarProduto>
    {
        private readonly IDispatcherBus _dispatcherBus;

        public CommandHandlers(IDispatcherBus dispatcherBus)
        {
            _dispatcherBus = dispatcherBus;
        }

        public bool Handle(CreateNewCommand command)
        {
            return true;
        }

        public Result Handle(DeleteCommand command)
        {
            return Result.Default;
        }

        public Result Handle(DesmebrarProduto command)
        {            
            _dispatcherBus.Event(new DesmembradoProdutoEvent());
            return Result.Default;
        }
    }
}