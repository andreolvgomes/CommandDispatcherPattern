using System;
using System.Collections.Generic;
using System.Text;

namespace MessagerBus.Queries
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery command);
    }
}
