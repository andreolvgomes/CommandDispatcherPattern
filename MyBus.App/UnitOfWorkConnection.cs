using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using Ninject;
using Ninject.Parameters;

namespace MyBus.App
{
    public class UnitOfWorkConnection
    {
        public static IKernel kernel;

        public static void Teste()
        {
            kernel = new StandardKernel();
            kernel.Bind<IExecuteHandler<SalvarExecute>>().To<SalvarHandler>();
            kernel.Bind<IExecuteHandler<EditarExecute>>().To<SalvarHandler>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IConnection>().To<Connection>();
            kernel.Bind<IConnectionRemoto>().ToMethod(ctx => new ConnectionRemoto("IConnectionRemoto"));

            //var handlerType = typeof(IExecuteHandler<>).MakeGenericType(new SalvarExecute().GetType());
            //Get(handlerType);
            IConnectionRemoto cnnrem = new ConnectionRemoto("ConnectionRemoto");
            IConnectionLocal cnnloc = new ConnectionLocal("ConnectionLocal");
            IUnitOfWork uow = kernel.Get<IUnitOfWork>();

            Execute(new SalvarExecute(), uow, cnnloc);
            Execute(new EditarExecute(), uow);
        }

        public static object Get(Type component)
        {
            var implementations = kernel.Get(component);
            return ResolveInstance(component, implementations.GetType());
        }

        private static object ResolveInstance(Type component, Type implementation)
        {
            return CreateNewInstance(component, implementation);
        }

        private static object CreateNewInstance(Type component, Type implementation)
        {
            var constructor = SelectConstructor(implementation);
            if (constructor == null)
            {
                throw new Exception("constructor is null");
            }

            var arguments = GetConstructorArguments(constructor.GetParameters());

            try
            {
                var instance = constructor.Invoke(arguments);// as INinjectComponent;
                return instance;
            }
            catch (TargetInvocationException ex)
            {
                return null;
            }
        }

        private static object[] GetConstructorArguments(ParameterInfo[] parameters)
        {
            if (parameters.Length == 0)
            {
                return Array.Empty<object>();
            }

            var arguments = new object[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                arguments[i] = Get(parameters[i].ParameterType);
            }

            return arguments;
        }

        private static ConstructorInfo SelectConstructor(Type implementation)
        {
            return implementation.GetConstructors().OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
        }

        //public static void Execute(IExecute execute, IUnitOfWork uow = null)
        public static void Execute(IExecute execute, params object[] constructors)
        {
            var handlerType = typeof(IExecuteHandler<>).MakeGenericType(execute.GetType());
            //dynamic handler = kernel.Get(handlerType);

            var implementations = kernel.Get(handlerType);
            var constructor = SelectConstructor(implementations.GetType());

            ParameterInfo[] parameters = constructor.GetParameters();
            List<ConstructorArgument> arguments = new List<ConstructorArgument>();
            List<ConstructorArgument> does_not_exists_arguments = new List<ConstructorArgument>();
            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameterInfo = parameters[i];
                var implemt = kernel.Get(parameterInfo.ParameterType);
                var param_constructor = constructors.ToList().FirstOrDefault(c => c.GetType().Equals(implemt.GetType()));
                if (param_constructor == null)
                    throw new Exception($"{parameterInfo.ParameterType} doesn't exists in constructor");

                if (implemt.GetType().Equals(param_constructor.GetType()))
                {
                    arguments.Add(new ConstructorArgument(parameterInfo.Name, param_constructor));
                }
            }

            ConstructorArgument[] myIntArray = new ConstructorArgument[arguments.Count];
            for (int i = 0; i < arguments.Count; i++)
                myIntArray[i] = arguments[i];

            dynamic handler2 = kernel.Get(handlerType, myIntArray);

            //handler.Handle((dynamic)execute, uow);
            handler2.Handle((dynamic)execute, null);
        }
    }

    public class SalvarHandler : IExecuteHandler<SalvarExecute>,
        IExecuteHandler<EditarExecute>
    {
        public SalvarHandler(IConnectionRemoto cnn, IUnitOfWork uow)
        {
        }

        public void Handle(EditarExecute execute, IUnitOfWork uow = null)
        {
        }

        public void Handle(SalvarExecute execute, IUnitOfWork uow = null)
        {
        }
    }

    public class SalvarExecute : IExecute { }
    public class EditarExecute : IExecute { }

    public class UnitOfWork : IUnitOfWork
    {
        public override string ToString() { return Id.ToString(); }

        private SqlConnection _cnn;
        public SqlConnection Cnn { get { return _cnn; } }

        private Guid _id;
        public Guid Id { get { return _id; } }

        public UnitOfWork()
        {
            _id = Guid.NewGuid();
            _cnn = new SqlConnection("");
        }
    }

    public interface IUnitOfWork
    {
        Guid Id { get; }
        SqlConnection Cnn { get; }
    }

    public interface IExecuteHandler<T> where T : IExecute
    {
        void Handle(T execute, IUnitOfWork uow = null);
    }

    public interface IExecute { }
}