using System;
using System.Collections.Generic;
using System.Text;
using MessagerBus;

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
