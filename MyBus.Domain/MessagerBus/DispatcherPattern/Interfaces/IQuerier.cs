using System;
using System.Collections.Generic;
using System.Text;

namespace MessagerBus.DispatcherPattern.Interfaces
{
    public interface IQuerier
    {
        TResult Query<TResult>(IQuery<TResult> _query);
    }
}
