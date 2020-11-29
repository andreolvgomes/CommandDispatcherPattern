using System;
using CommandDispatcher;
using CommandDispatcher.DispatcherPattern;
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
            _kernel.Bind<IDispatcherInvoke>().To<DispatcherInvoke>();
            _kernel.Bind<IDispatcher>().To<Dispatcher>();
            _kernel.Bind<IServiceContainer>().To<ServiceInstanceKernel>();

            _kernel.Bind<IConnectionLocal>().ToMethod(ctx => new ConnectionLocal("ConnectionLocal: " + Guid.NewGuid().ToString()));
            _kernel.Bind<IConnectionRemoto>().ToMethod(ctx => new ConnectionRemoto("ConnectionRemoto: " + Guid.NewGuid().ToString()));

            _kernel.Bind(typeof(ICommandHandler<CreateNewProduct>)).To(typeof(CreateHandler));
            _kernel.Bind(typeof(ICommandHandler<EditNewProduct>)).To(typeof(EditHandler));

            IDispatcher dispatcher = _kernel.Get<IDispatcher>();

            IConnectionRemoto cnn = _kernel.Get<IConnectionRemoto>();
            IConnectionLocal cnnlocal = _kernel.Get<IConnectionLocal>();

            dispatcher.Command(new CreateNewProduct(), cnn, cnnlocal);
            dispatcher.Command(new CreateNewProduct(), cnn, cnnlocal);
            dispatcher.Command(new EditNewProduct(), cnn, cnnlocal);
        }

        public T Get<T>() where T : class
        {
            return _kernel.Get<T>();
        }
    }

    #region classes de testes

    // commands
    public class CreateNewProduct : ICommand { }
    public class EditNewProduct : ICommand { }

    public class CreateHandler : ICommandHandler<CreateNewProduct>
    {
        public CreateHandler(IConnectionRemoto cnnremo, IConnectionLocal cnnlocal)
        {
        }

        public Result Handle(CreateNewProduct command)
        {
            return Result.Default;
        }
    }

    public class EditHandler : ICommandHandler<EditNewProduct>
    {
        public EditHandler(IConnectionRemoto cnnremo)
        {
        }

        public Result Handle(EditNewProduct command)
        {
            return Result.Default;
        }
    }

    #endregion
}