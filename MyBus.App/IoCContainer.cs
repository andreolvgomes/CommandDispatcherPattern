using System;
using CommandDispatcher;
using CommandDispatcher.Pattern;
using My.Tests.Events;
using My.Tests.Queries;
using MyBus.Tests.Commands;
using MyBus.Tests.Events;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Lifestyles;

namespace MyBus.App
{
    public class IoCContainer
    {
        private static IoCContainer _instance = null;

        public static IoCContainer Ins
        {
            get
            {
                if (_instance == null)
                    _instance = new IoCContainer();
                return _instance;
            }
        }

        private readonly Container _container;

        private IoCContainer()
        {
            _container = new Container();
        }

        public void Init()
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            _container.Register<IDispatcherInvoke, DispatcherInvoke>();
            _container.Register<IDispatcher, Dispatcher>();
            _container.Register<IServiceContainer, ServiceInstanceContainer>();

            //_container.Register<ClassWithDisposable>();
            _container.RegisterDisposableTransient<Produtos>();

            _container.RegisterDisposableTransient<IClassWithDisposable, ClassWithDisposable>();
            //_container.Register<IClassWithDisposable, ClassWithDisposable>();

            //_container.Register(typeof(ICommandHandler<>), new[] { typeof(ICommandHandler<>).Assembly });
            //_container.Register(typeof(ICommandHandler<,>), new[] { typeof(ICommandHandler<,>).Assembly });

            _container.Register(typeof(ICommandHandler<CreateNewCommand, bool>), typeof(CommandsHandlers));
            _container.Register(typeof(IEventHandler<CreateNewEvent>), typeof(EventsHandlers));
            _container.Register(typeof(IQueryHandler<GetProductQuery, string>), typeof(QueriesHandlers));

            var test1 = _container.GetInstance<IClassWithDisposable>();
            var test2 = _container.GetInstance<Produtos>();

            //ClassWithDisposable test2 = _container.GetInstance<ClassWithDisposable>();

            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                var x = _container.GetInstance<IClassWithDisposable>();
                var y = _container.GetInstance<Produtos>();
            }

            _container.Verify();
        }

        public T Get<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }

    public static class TEste
    {
        public static void RegisterDisposableTransient<TConcrete>(this Container container)
                where TConcrete : class, IDisposable
        {
            var scoped = Lifestyle.Scoped;
            var reg = Lifestyle.Transient.CreateRegistration<TConcrete>(container);

            reg.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "suppres");

            container.AddRegistration(typeof(TConcrete), reg);
            container.RegisterInitializer<TConcrete>(o => scoped.RegisterForDisposal(container, o));
        }

        public static void RegisterDisposableTransient<TService, TImplementation>(this Container c)
                where TImplementation : class, IDisposable, TService
                where TService : class
        {
            var scoped = Lifestyle.Scoped;
            var createRegistration = Lifestyle.Transient.CreateRegistration<TImplementation>(c);

            createRegistration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "ignore");
            c.AddRegistration(typeof(TService), createRegistration);
            c.RegisterInitializer<TImplementation>(o => scoped.RegisterForDisposal(c, o));
        }
    }
}