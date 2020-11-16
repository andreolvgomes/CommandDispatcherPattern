using MessagerBus;
using MessagerBus.DispatcherPattern;
using Microsoft.Extensions.DependencyInjection;
using My.Tests.Events;
using My.Tests.Queries;
using MyBus.Tests.Commands;
using MyBus.Tests.Events;
using SimpleInjector;

namespace MyBus.App
{
    public class IoCServiceCollection
    {
        private static IoCServiceCollection _instance = null;

        public static IoCServiceCollection Ins
        {
            get
            {
                if (_instance == null)
                    _instance = new IoCServiceCollection();
                return _instance;
            }
        }

        private ServiceProvider _serviceProvider;

        private IoCServiceCollection()
        {
        }

        public void Init()
        {
            ServiceCollection service = new ServiceCollection();
            service.AddScoped<IMessager, Messager>();
            service.AddScoped<IDispatcher, Dispatcher>();
            service.AddScoped<IServiceInstance, ServiceInstanceContainer>();

            //service.AddScoped(typeof(ICommandHandler<>), new[] { typeof(ICommandHandler<>).Assembly });
            //service.AddScoped(typeof(ICommandHandler<,>), new[] { typeof(ICommandHandler<,>).Assembly });
            service.AddScoped(typeof(ICommandHandler<CreateNewCommand, bool>), typeof(CommandsHandlers));
            service.AddScoped(typeof(IEventHandler<CreateNewEvent>), typeof(EventsHandlers));
            service.AddScoped(typeof(IQueryHandler<GetProductQuery, string>), typeof(QueriesHandlers));

            _serviceProvider = service.BuildServiceProvider();

            // https://csharp.christiannagel.com/2018/08/29/scopes/
            //using (IServiceScope scope = _serviceProvider.CreateScope())
            //{
            //    IService serv = scope.ServiceProvider.GetService<IService>();
            //    serv.NewService();
            //}
        }

        public T Get<T>() where T : class
        {
            return _serviceProvider.GetService<T>();
        }
    }
}