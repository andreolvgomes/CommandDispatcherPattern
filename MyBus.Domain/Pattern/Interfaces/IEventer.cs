using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDispatcher.Pattern.Interfaces
{
    public interface IEventer
    {
        TResult Event<TResult>(IEvent<TResult> _event);
        void Event(IEvent _event);
    }
}
