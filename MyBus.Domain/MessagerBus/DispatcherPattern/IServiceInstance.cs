using System;

namespace MessagerBus
{
    public interface IServiceInstance
    {
        object GetInstance(Type serviceType);
        TService GetInstance<TService>() where TService : class;
    }
}