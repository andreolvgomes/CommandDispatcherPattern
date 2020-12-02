using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using CommandDispatcher;

namespace My.Tests.Queries
{
    public class QueriesHandlers : IQueryHandler<GetProductQuery, string>
    {
        public string Handle(GetProductQuery query, SqlTransaction transaction = null)
        {
            return "IQueryHandler";
        }
    }
}
