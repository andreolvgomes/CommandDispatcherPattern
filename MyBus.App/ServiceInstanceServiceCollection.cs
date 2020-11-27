using System;
using System.Collections.Generic;
using System.Text;
using MessagerBus;
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

        public object GetInstance(Type serviceType, params object[] param_constructor)
        {
            return _serviceProvider.GetService(serviceType);
        }

        public TService GetInstance<TService>(params object[] param_constructor) where TService : class
        {
            return _serviceProvider.GetService<TService>();
        }
    }
}