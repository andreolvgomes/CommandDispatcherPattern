using System;
using System.Collections.Generic;
using System.Text;

namespace MessagerBus.DispatcherPattern.Interfaces
{
    public interface ICommander
    {
        TResult Command<TResult>(ICommand<TResult> command, params object[] param_constructor);
        void Command(ICommand command, params object[] param_constructor);
    }
}
