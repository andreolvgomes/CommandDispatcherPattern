using System;
using System.Collections.Generic;
using System.Text;
using MessagerBus;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace MyBus.App
{
    public class ServiceInstanceServiceCollection : IServiceInstance
    {
        private readonly ServiceProvider _serviceProvider;

        public ServiceInstanceServiceCollection(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TService GetInstance<TService>() where TService : class
        {
            return _serviceProvider.GetService<TService>();
        }

        public object GetInstance(Type serviceType)
        {
            return _serviceProvider.GetService(serviceType);
        }
    }
}