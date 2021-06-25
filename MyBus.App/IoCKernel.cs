using System;
using System.Data.Common;
using System.Data.SqlClient;
using CommandDispatcher;
using Domain.Messenger;
using MyBus.App.Functions;
using MyBus.App.Functions.Handlers;
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

            _kernel.Bind(typeof(IFunctionHandler<ConcateNameFunction, string>)).To(typeof(ConcateNameFunctionHandler));
            _kernel.Bind(typeof(IFunctionHandler<ReturnResultDefaultFunction>)).To(typeof(ReturnResultDefaultFunctionHandler));
        }

        public T Instance<T>() where T : class
        {
            return _kernel.Get<T>();
        }

        public static T Get<T>() where T : class
        {
            return IoCKernel.Ins.Instance<T>();
        }
    }
}