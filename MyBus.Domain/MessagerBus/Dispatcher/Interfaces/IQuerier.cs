using System;
using System.Collections.Generic;
using System.Text;
using MessagerBus.Queries;

namespace MessagerBus.Interfaces
{
    public interface IQuerier
    {
        TResult Query<TResult>(IQuery<TResult> _query);
    }
}
