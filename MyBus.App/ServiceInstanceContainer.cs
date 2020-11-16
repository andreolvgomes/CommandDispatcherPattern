using System;
using System.Collections.Generic;
using System.Text;
using MessagerBus;
using SimpleInjector;

namespace MyBus.App
{
    public class ServiceInstanceContainer : IServiceInstance
    {
        private readonly Container _container;

        public ServiceInstanceContainer(Container container)
        {
            _container = container;
        }

        public TService GetInstance<TService>() where TService : class
        {
            return _container.GetInstance<TService>();
        }

        public object GetInstance(Type serviceType)
        {
            return _container.GetInstance(serviceType);
        }
    }
}