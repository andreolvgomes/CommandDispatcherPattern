using System;
using System.Collections.Generic;
using System.Text;

namespace MessagerBus.Interfaces
{
    public interface IEventer
    {
        TResult Event<TResult>(IEvent<TResult> _event);
        void Event(IEvent _event);
    }
}
