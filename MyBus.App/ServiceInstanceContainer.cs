using System;
using System.Collections.Generic;
using System.Text;
using CommandDispatcher;
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object GetInstance(Type serviceType, params object[] params_constructor)
        {
            return _container.GetInstance(serviceType);
        }

        public TService GetInstance<TService>(params object[] params_constructor) where TService : class
        {
            return _container.GetInstance<TService>();
        }
    }
}