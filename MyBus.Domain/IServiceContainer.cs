using System;

namespace CommandDispatcher
{
    public interface IServiceContainer
    {
        object GetInstance(Type serviceType, object[] params_constructor = null);
        //TService GetInstance<TService>(object[] params_constructor = null) where TService : class;
    }
}