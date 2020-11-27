using System;
using MessagerBus;
using MessagerBus.DispatcherPattern;
using My.Tests.Events;
using My.Tests.Queries;
using MyBus.Tests.Commands;
using MyBus.Tests.Events;
using Ninject;
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
            _kernel.Bind<IMessager>().To<Messager>();
            _kernel.Bind<IDispatcher, Dispatcher>();
            _kernel.Bind<IServiceContainer, ServiceInstanceContainer>();

            //_container.Bind<IClassWithDisposable, ClassWithDisposable>();

            //_container.Bind(typeof(ICommandHandler<>), new[] { typeof(ICommandHandler<>).Assembly });
            //_container.Bind(typeof(ICommandHandler<,>), new[] { typeof(ICommandHandler<,>).Assembly });

            _kernel.Bind(typeof(ICommandHandler<CreateNewCommand, bool>), typeof(CommandsHandlers));
            _kernel.Bind(typeof(IEventHandler<CreateNewEvent>), typeof(EventsHandlers));
            _kernel.Bind(typeof(IQueryHandler<GetProductQuery, string>), typeof(QueriesHandlers));

            var test1 = _kernel.Get<IClassWithDisposable>();
            var test2 = _kernel.Get<Produtos>();
        }

        public T Get<T>() where T : class
        {
            return _kernel.Get<T>();
        }
    }
}