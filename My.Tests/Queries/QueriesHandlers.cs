using System;
using System.Collections.Generic;
using System.Text;
using CommandDispatcher;

namespace My.Tests.Queries
{
    public class QueriesHandlers : IQueryHandler<GetProductQuery, string>
    {
        public string Handle(GetProductQuery command)
        {
            return "IQueryHandler";
        }
    }
}
