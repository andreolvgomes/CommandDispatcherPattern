using CommandDispatcher;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBus.App.Functions.Handlers
{
    public class ConcateNameFunctionHandler : IFunctionHandler<ConcateNameFunction, string>
    {
        public string Handle(ConcateNameFunction function, SqlTransaction transaction = null)
        {
            return $"{function.FirstName} {function.LastName}";
        }
    }
}