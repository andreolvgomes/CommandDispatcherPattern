using AutoMapper;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyBus.App
{
    public class KernelArguments
    {
        private static readonly Dictionary<Type, object> instances = new Dictionary<Type, object>();

        public class MappingConfiguration
        {
            public static MapperConfiguration Register()
            {
                return new MapperConfiguration(cfg =>
                {
                });
            }
        }

        private static IKernel _kernel;

        public static void Execute()
        {
            AtStart();

            var cnn = _kernel.Get<IConnect>();
            var pedido = Get<IPedidos>(new object[] { cnn });
            var pedido2 = Get<IPedidos>();
            Console.ReadKey();
        }

        private static void AtStart()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IPedidos>().To<Pedidos>();
            _kernel.Bind<IClientes>().To<Clientes>();
            _kernel.Bind<IProduct>().To<Product>();
            _kernel.Bind<IConnect>().ToMethod(ctx => new Connect("str"));

            var config = MappingConfiguration.Register();
            _kernel.Bind<IMapper>().ToMethod(context => { return config.CreateMapper(); }).InSingletonScope();
        }

        private static T Get<T>(object[] params_constructor = null)
        {
            Type serviceType = typeof(T);
            if (params_constructor != null && params_constructor.Length > 0)
                return (T)Instance(serviceType, serviceType, params_constructor: params_constructor);
            return (T)_kernel.Get(serviceType);
        }

        private static object Instance(Type type, Type typeAtMain, object[] params_constructor)
        {
            if (type.ToString().Equals("AutoMapper.IMapper"))
                return _kernel.Get(type);

            var arguments = new List<ConstructorArgument>();

            var implementation = GetImplementation(type);
            if (implementation == null)
                return null;

            var constructors = SelectConstructor(implementation.GetType());
            if (constructors == null)
                return _kernel.Get(type);

            List<object> implementations_constr = new List<object>();
            ParameterInfo[] parameters = constructors.GetParameters();

            foreach (var param in parameters)
            {
                var implemt = Instance(param.ParameterType, typeAtMain, params_constructor);
                if (implemt == null) continue;

                implementations_constr.Add(implemt);

                var arg_constructor = params_constructor.ToList().FirstOrDefault(c => c.GetType().Equals(implemt.GetType()));
                if (arg_constructor != null)
                    arguments.Add(new ConstructorArgument(param.Name, arg_constructor));
                else
                    arguments.Add(new ConstructorArgument(param.Name, implemt));
            }

            if (arguments.Count == 0)
                return _kernel.Get(type);

            // Valida os parâmetros do construtor da implementação principal. 
            // A classe para qual realmente foi passado o parâmetros, as outras são secundárias, se tem ou não, não importa
            if (type.Equals(typeAtMain))
                CheckImplementationsThrow(implementations_constr, implementation, params_constructor);

            return _kernel.Get(type, arguments.ToArray());
        }

        private static object GetImplementation(Type component)
        {
            if (IsSimple(component)) return null;

            object implementation;
            if (!instances.TryGetValue(component, out implementation))
            {
                implementation = _kernel.Get(component);
                instances.Add(component, implementation);
            }
            return implementation;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/863881/how-do-i-tell-if-a-type-is-a-simple-type-i-e-holds-a-single-value/863944
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsSimple(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return IsSimple(type.GetGenericArguments()[0]);
            }
            return type.IsPrimitive
              || type.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(decimal));
        }

        private static void CheckImplementationsThrow(List<object> implementations_constr, object implementation, object[] params_constructor)
        {
            // valid
            foreach (var item in params_constructor)
            {
                // lança exceção caso o obj passado para o construtor não exista como argumento na implementação
                if (implementations_constr.FirstOrDefault(c => c.GetType().Equals(item.GetType())) == null)
                    throw new Exception($"{implementation.GetType()} doesn't contains '{item.GetType()}' in your constructor");
            }
        }

        private static ConstructorInfo SelectConstructor(Type implementation)
        {
            return implementation.GetConstructors().OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
        }
    }

    public interface IPedidos
    {
        IConnect Cnn { get; set; }
        IClientes Clientes { get; set; }
        IProduct Produtos { get; set; }
    }

    public class Pedidos : IPedidos
    {
        public IConnect Cnn { get; set; }
        public IClientes Clientes { get; set; }
        public IProduct Produtos { get; set; }

        //public Pedidos(IConnect cnn, IClientes clientes, IProduct produtos)
        public Pedidos(IConnect cnn, IClientes clientes, IMapper mapper)
        {
            Cnn = cnn;
            Clientes = clientes;
            //Produtos = produtos;

            Console.WriteLine(Cnn.Id);
            Console.WriteLine(Clientes.Cnn.Id);
            //Console.WriteLine(Produtos.Cnn.Id);
        }
    }

    public interface IClientes
    {
        IConnect Cnn { get; set; }
    }
    public class Clientes : IClientes
    {
        public IConnect Cnn { get; set; }
        public Clientes(IConnect cnn) { Cnn = cnn; }
    }

    public interface IProduct
    {
        IConnect Cnn { get; set; }
    }
    public class Product : IProduct
    {
        public IConnect Cnn { get; set; }
        public Product(IConnect cnn) { Cnn = cnn; }
    }

    public interface IConnect
    {
        string Id { get; }
    }
    public class Connect : IConnect
    {
        private string _id;
        public string Id
        {
            get
            {
                if (string.IsNullOrEmpty(_id))
                    _id = Guid.NewGuid().ToString().Substring(10, 10);
                return _id;
            }
        }

        public Connect(string str)
        {

        }
    }
}