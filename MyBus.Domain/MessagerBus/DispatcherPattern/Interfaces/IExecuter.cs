using System;
using System.Collections.Generic;
using System.Text;

namespace MessagerBus.DispatcherPattern.Interfaces
{
    public interface IExecuter
    {
        TResult Execute<TResult>(ICommand<TResult> command);
        void Execute(ICommand command);
    }
}