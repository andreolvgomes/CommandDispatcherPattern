using My.Tests.Commands;
using My.Tests.Events;
using MessagerBus;
using MessagerBus.Dispatcher;
using MyBus.Tests.Commands;
using MyBus.Tests.Events;
using SimpleInjector;

namespace MyBus.App
{
    public class IoC
    {
        private static IoC _instance = null;

        public static IoC Ins
        {
            get
            {
                if (_instance == null)
                    _instance = new IoC();
                return _instance;
            }
        }

        private readonly Container _container;

        private IoC()
        {
            _container = new Container();
        }

        public void Init()
        {
            _container.Register<IDispatcherBus, DispatcherBus>();

            //_container.Register(typeof(ICommandHandler<>), new[] { typeof(ICommandHandler<>).Assembly });
            //_container.Register(typeof(ICommandHandler<,>), new[] { typeof(ICommandHandler<,>).Assembly });

            _container.Register<IDispatcher, Dispatcher>();

            _container.Register(typeof(ICommandHandler<DeleteCommand>), typeof(CommandHandlers));
            _container.Register(typeof(ICommandHandler<CreateNewCommand, bool>), typeof(CommandHandlers));
            _container.Register(typeof(ICommandHandler<DesmebrarProduto>), typeof(CommandHandlers));

            _container.Register(typeof(IEventHandler<DesmembradoProdutoEvent>), typeof(EventsHandlers));
        }

        public T Get<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}