using MessagerBus;
using MessagerBus.DispatcherPattern;
using My.Tests.Events;
using My.Tests.Queries;
using MyBus.Tests.Commands;
using MyBus.Tests.Events;
using SimpleInjector;

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
            _container.Register<IMessager, Messager>();
            _container.Register<IDispatcher, Dispatcher>();
            _container.Register<IServiceInstance, ServiceInstanceContainer>();

            //_container.Register(typeof(ICommandHandler<>), new[] { typeof(ICommandHandler<>).Assembly });
            //_container.Register(typeof(ICommandHandler<,>), new[] { typeof(ICommandHandler<,>).Assembly });

            _container.Register(typeof(ICommandHandler<CreateNewCommand, bool>), typeof(CommandsHandlers));
            _container.Register(typeof(IEventHandler<CreateNewEvent>), typeof(EventsHandlers));
            _container.Register(typeof(IQueryHandler<GetProductQuery, string>), typeof(QueriesHandlers));
        }

        public T Get<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}