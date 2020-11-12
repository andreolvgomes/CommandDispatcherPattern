using System;
using System.Collections.Generic;
using System.Text;

namespace MessagerBus.Interfaces
{
    public interface IExecuter
    {
        TResult Execute<TResult>(ICommand<TResult> request);
        void Execute(ICommand request);
    }
}
