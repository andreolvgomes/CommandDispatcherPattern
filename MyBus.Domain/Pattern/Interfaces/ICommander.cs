using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace CommandDispatcher.Pattern.Interfaces
{
    public interface ICommander
    {
        TResult Command<TResult>(ICommand<TResult> command, object[] params_constructor = null, SqlTransaction transaction = null);
        void Command(ICommand command, object[] params_constructor = null, SqlTransaction transaction = null);
    }
}
