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
    public class Kernel_ArgumentsConstructor
    {
        public static IKernel kernel;

        public static void Teste()
        {
            kernel = new StandardKernel();
            kernel.Bind<IExecuteHandler<SalvarExecute>>().To<SalvarHandler>();
            kernel.Bind<IExecuteHandler<EditarExecute>>().To<SalvarHandler>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IConnection>().To<Connection>();
            kernel.Bind<IConnectionRemoto>().ToMethod(ctx => new ConnectionRemoto(Guid.NewGuid().ToString()));

            IConnectionRemoto cnnrem = new ConnectionRemoto(Guid.NewGuid().ToString());
            IConnectionLocal cnnloc = new ConnectionLocal(Guid.NewGuid().ToString());
            IUnitOfWork uow = kernel.Get<IUnitOfWork>();

            Execute(new SalvarExecute(), cnnrem);
            Execute(new EditarExecute(), cnnrem);
        }

        public static void Execute(IExecute execute, params object[] param_constructors)
        {
            var component = typeof(IExecuteHandler<>).MakeGenericType(execute.GetType());

            List<ConstructorArgument> arguments = new List<ConstructorArgument>();
            if (param_constructors.Length > 0)
                arguments = Arguments(param_constructors, component);

            dynamic implementation = kernel.Get(component, arguments.ToArray());
            implementation.Handle((dynamic)execute, null);
        }

        private static List<ConstructorArgument> Arguments(object[] param_constructors, Type component)
        {
            var implementation = kernel.Get(component);
            var constructor = SelectConstructor(implementation.GetType());

            ParameterInfo[] parameters = constructor.GetParameters();
            List<ConstructorArgument> arguments = new List<ConstructorArgument>();
            List<object> implementations = new List<object>();

            foreach (ParameterInfo parameter in parameters)
            {
                var implemt = kernel.Get(parameter.ParameterType);
                implementations.Add(implemt);

                var param_constructor = param_constructors.ToList().FirstOrDefault(c => c.GetType().Equals(implemt.GetType()));
                if (param_constructor == null)
                    continue;

                if (implemt.GetType().Equals(param_constructor.GetType()))
                    arguments.Add(new ConstructorArgument(parameter.Name, param_constructor));
            }

            // valid
            foreach (var item in param_constructors)
            {
                // lança exceção caso o obj passado para o construtor não exista como argumento na implementação
                if (implementations.FirstOrDefault(c => c.GetType().Equals(item.GetType())) == null)
                    throw new Exception($"{implementation.GetType()} doesn't exists '{item.GetType()}' in constructor");
            }

            return arguments;
        }

        private static ConstructorInfo SelectConstructor(Type implementation)
        {
            return implementation.GetConstructors().OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
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