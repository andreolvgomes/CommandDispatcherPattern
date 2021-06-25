using CommandDispatcher;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBus.App.Functions.Handlers
{
    public class ReturnResultDefaultFunctionHandler : IFunctionHandler<ReturnResultDefaultFunction>
    {
        public Result Handle(ReturnResultDefaultFunction function, SqlTransaction transaction = null)
        {
            return Result.Default;
        }
    }
}