using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MessagerBus;
using Ninject;
using Ninject.Parameters;
using SimpleInjector;

namespace MyBus.App
{
    public class ServiceInstanceKernel : IServiceContainer
    {
        private readonly IKernel _kernel;

        public ServiceInstanceKernel(IKernel kernel)
        {
            _kernel = kernel;
        }

        public object GetInstance(Type serviceType, params object[] param_constructor)
        {
            List<ConstructorArgument> arguments = new List<ConstructorArgument>();
            if (param_constructor.Length > 0)
                arguments = Arguments(param_constructor, serviceType);

            return _kernel.Get(serviceType, arguments.ToArray());
        }

        //public TService GetInstance<TService>(params object[] param_constructor) where TService : class
        //{
        //    List<ConstructorArgument> arguments = new List<ConstructorArgument>();
        //    if (param_constructor.Length > 0)
        //        arguments = Arguments(param_constructor, serviceType);

        //    return _kernel.GetInstance<TService>();
        //}

        private List<ConstructorArgument> Arguments(object[] param_constructors, Type component)
        {
            var implementation = _kernel.Get(component);
            var constructor = SelectConstructor(implementation.GetType());

            ParameterInfo[] parameters = constructor.GetParameters();
            List<ConstructorArgument> arguments = new List<ConstructorArgument>();
            List<object> implementations = new List<object>();

            foreach (ParameterInfo parameter in parameters)
            {
                var implemt = _kernel.Get(parameter.ParameterType);
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

        private ConstructorInfo SelectConstructor(Type implementation)
        {
            return implementation.GetConstructors().OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
        }
    }
}