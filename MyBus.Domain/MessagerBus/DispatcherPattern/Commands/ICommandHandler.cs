using System;

namespace MessagerBus
{
    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Result>
        where TCommand : ICommand
    {
    }

    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        TResult Handle(TCommand command);
    }
}