using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using CommandDispatcher;

namespace My.Tests.Queries
{
    public class QueriesHandlers : IQueryHandler<GetProductQuery, string>
    {
        public string Handle(GetProductQuery query, DbTransaction transaction = null)
        {
            return "IQueryHandler";
        }
    }
}
