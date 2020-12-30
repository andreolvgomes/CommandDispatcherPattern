using System;
using System.Collections.Generic;
using System.Text;
using CommandDispatcher;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace MyBus.App
{
    public class ServiceInstanceServiceCollection : IServiceContainer
    {
        private readonly ServiceProvider _serviceProvider;

        public ServiceInstanceServiceCollection(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object GetInstance(Type serviceType, params object[] params_constructor)
        {
            return _serviceProvider.GetService(serviceType);
        }

        public TService GetInstance<TService>(params object[] params_constructor) where TService : class
        {
            return _serviceProvider.GetService<TService>();
        }
    }
}