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
            _kernel.Bind<IServiceContainer>().To<ServiceContainerKernel>();

            _kernel.Bind<IConnectionLocal>().ToMethod(ctx => new ConnectionLocal("ConnectionLocal: " + Guid.NewGuid().ToString()));
            _kernel.Bind<IConnectionRemoto>().ToMethod(ctx => new ConnectionRemoto("ConnectionRemoto: " + Guid.NewGuid().ToString()));

            _kernel.Bind(typeof(ICommandHandler<CreateNewProduct>)).To(typeof(CreateHandler));
            _kernel.Bind(typeof(ICommandHandler<EditNewProduct>)).To(typeof(EditHandler));

            // verify binding
            //foreach (var item in _kernel.GetBindings())
            //    _kernel.Get(item);

            IDispatcher dispatcher = _kernel.Get<IDispatcher>();

            IConnectionRemoto cnn = _kernel.Get<IConnectionRemoto>();
            IConnectionLocal cnnlocal = _kernel.Get<IConnectionLocal>();

            //var adsf = _kernel.Get<CreateNewProduct>();
            //var adsf2 = _kernel.Get<CreateNewProduct>();
            //var adsf3 = _kernel.Get<CreateNewProduct>();

            dispatcher.Command(new CreateNewProduct(), new object[] { cnn, cnnlocal });

            var x = _kernel.Get<EditNewProduct>();

            dispatcher.Command(new EditNewProduct());

            using (IActivationBlock scolpe = _kernel.BeginBlock())
            {
                cnn = scolpe.Get<IConnectionRemoto>();
                cnnlocal = scolpe.Get<IConnectionLocal>();
            }

            //dispatcher.Command(new EditNewProduct(), new object[] { cnn, cnnlocal });
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
        private IDispatcher dispatcher;
        private IConnectionRemoto cnnremo;

        public CreateHandler(IDispatcher dispatcher,IConnectionRemoto cnnremo, IConnectionLocal cnnlocal)
        {
            this.dispatcher = dispatcher;
            this.cnnremo = cnnremo;
        }

        public Result Handle(CreateNewProduct command, SqlTransaction transaction = null)
        {
            //dispatcher.Command(new EditNewProduct(), new object[] { this.cnnremo });
            return Result.Default;
        }
    }

    public class EditHandler : ICommandHandler<EditNewProduct>
    {
        public EditHandler()
        {
        }

        public Result Handle(EditNewProduct command, SqlTransaction transaction = null)
        {
            return Result.Default;
        }
    }

    #endregion
}