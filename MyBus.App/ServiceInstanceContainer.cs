using System;
using System.Collections.Generic;
using System.Text;
using MessagerBus;
using SimpleInjector;

namespace MyBus.App
{
    public class ServiceInstanceContainer : IServiceContainer
    {
        private readonly Container _container;

        public ServiceInstanceContainer(Container container)
        {
            _container = container;
        }

        public object GetInstance(Type serviceType, params object[] param_constructor)
        {
            return _container.GetInstance(serviceType);
        }

        public TService GetInstance<TService>(params object[] param_constructor) where TService : class
        {
            return _container.GetInstance<TService>();
        }
    }
}