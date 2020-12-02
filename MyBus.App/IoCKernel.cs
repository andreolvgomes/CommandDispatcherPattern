using System;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using CommandDispatcher;
using CommandDispatcher.Pattern;
using My.Tests.Events;
using My.Tests.Queries;
using MyBus.Tests.Commands;
using MyBus.Tests.Events;
using Ninject;
using Ninject.Infrastructure;
using Ninject.Planning.Bindings;
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
            _kernel.Bind<IServiceContainer>().To<IServiceContainerKernel>();

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

            dispatcher.Command(new CreateNewProduct(), new object[] { cnn, cnnlocal });
            dispatcher.Command(new CreateNewProduct(), new object[] { cnn, cnnlocal });
            dispatcher.Command(new EditNewProduct(), new object[] { cnn, cnnlocal });
        }

        public T Get<T>() where T : class
        {
            return _kernel.Get<T>();
        }
    }

    public static class KernelExtensions
    {
        public static void Verify(this IKernel kernel)
        {
            var bindings = GetBindings(kernel);
            foreach (var item in bindings)
            {
                var j = kernel.Get(item);
            }
        }

        public static Type[] GetBindings(this IKernel kernel)
        {
            return ((Multimap<Type, IBinding>)typeof(KernelBase)
                .GetField("bindings", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(kernel)).Select(x => x.Key).ToArray();
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

        public Result Handle(CreateNewProduct command, DbTransaction transaction = null)
        {
            return Result.Default;
        }
    }

    public class EditHandler : ICommandHandler<EditNewProduct>
    {
        public EditHandler(IConnectionRemoto cnnremo)
        {
        }

        public Result Handle(EditNewProduct command, DbTransaction transaction = null)
        {
            return Result.Default;
        }
    }

    #endregion
}