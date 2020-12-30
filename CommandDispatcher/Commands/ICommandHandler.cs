using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CommandDispatcher
{
    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Result>
        where TCommand : ICommand
    {
    }

    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        TResult Handle(TCommand command, SqlTransaction transaction = null);
    }
}