using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDispatcher.Pattern.Interfaces
{
    public interface ICommander
    {
        TResult Command<TResult>(ICommand<TResult> command, params object[] params_constructor);
        void Command(ICommand command, params object[] params_constructor);
    }
}
