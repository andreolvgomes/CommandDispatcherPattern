using System;
using System.Data.Common;
using System.Data.SqlClient;
using CommandDispatcher;
using CommandDispatcher;
using Domain.Messenger;
using My.Tests.Events;
using My.Tests.Queries;
using MyBus.Tests.Commands;
using MyBus.Tests.Events;
using Ninject;
using Ninject.Activation.Blocks;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Lifestyles;

namespace MyBus.App
{
    public class IoCKernel
    {
        private static IoCKernel _instance = null;

        public static IoCKernel Ins
        {
            get
            {
                if (_instance == null)
                    _instance = new IoCKernel();
                return _instance;
            }
        }

        private readonly IKernel _kernel;

        private IoCKernel()
        {
            _kernel = new StandardKernel();
        }

        public void Init()
        {
            // requared
            _kernel.Bind<IDispatcherInvoke>().To<DispatcherInvoke>();
            _kernel.Bind<IDispatcher>().To<Dispatcher>();
            _kernel.Bind<IServiceContainer>().To<ServiceContainerNinject>();

            //_kernel.Bind(typeof(ICommandHandler<CreateNewProduct>)).To(typeof(CreateHandler));
            //_kernel.Bind(typeof(ICommandHandler<EditNewProduct>)).To(typeof(EditHandler));
        }

        public T Get<T>() where T : class
        {
            return _kernel.Get<T>();
        }
    }
}