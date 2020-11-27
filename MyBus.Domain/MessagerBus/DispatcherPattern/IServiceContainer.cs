using System;

namespace MessagerBus
{
    public interface IServiceContainer
    {
        object GetInstance(Type serviceType, params object[] param_constructor);
        //TService GetInstance<TService>(params object[] param_constructor) where TService : class;
    }
}