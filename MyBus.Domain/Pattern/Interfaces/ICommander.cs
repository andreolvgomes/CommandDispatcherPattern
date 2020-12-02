using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDispatcher.Pattern.Interfaces
{
    public interface ICommander
    {
        TResult Command<TResult>(ICommand<TResult> command, object[] params_constructor = null);
        void Command(ICommand command, object[] params_constructor = null);
    }
}
