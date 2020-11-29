using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CommandDispatcher;
using Ninject;
using Ninject.Parameters;

namespace MyBus.App
{
    public class ServiceInstanceKernel : IServiceContainer
    {
        private readonly Dictionary<Type, object> instances = new Dictionary<Type, object>();

        private readonly IKernel _kernel;

        public ServiceInstanceKernel(IKernel kernel)
        {
            _kernel = kernel;
        }

        public object GetInstance(Type serviceType, params object[] params_constructor)
        {
            List<ConstructorArgument> arguments = new List<ConstructorArgument>();
            if (params_constructor.Length > 0)
                arguments = Arguments(serviceType, params_constructor);

            return _kernel.Get(serviceType, arguments.ToArray());
        }

        //public TService GetInstance<TService>(params object[] param_constructor) where TService : class
        //{
        //    List<ConstructorArgument> arguments = new List<ConstructorArgument>();
        //    if (param_constructor.Length > 0)
        //        arguments = Arguments(param_constructor, serviceType);

        //    return _kernel.GetInstance<TService>();
        //}

        private List<ConstructorArgument> Arguments(Type component, object[] params_constructor)
        {
            object implementation = GetImplementation(component);
            var constructor = SelectConstructor(implementation.GetType());

            ParameterInfo[] parameters = constructor.GetParameters();
            List<ConstructorArgument> arguments = new List<ConstructorArgument>();
            List<object> implementations_constr = new List<object>();

            foreach (ParameterInfo parameter in parameters)
            {
                var implemt = _kernel.Get(parameter.ParameterType);
                implementations_constr.Add(implemt);

                var param_constructor = params_constructor.ToList().FirstOrDefault(c => c.GetType().Equals(implemt.GetType()));
                if (param_constructor == null)
                    continue;

                if (implemt.GetType().Equals(param_constructor.GetType()))
                    arguments.Add(new ConstructorArgument(parameter.Name, param_constructor));
            }

            // valid
            CheckImplementationsThrow(implementations_constr, implementation, params_constructor);            

            return arguments;
        }

        private void CheckImplementationsThrow(List<object> implementations_constr, object implementation, object[] param_constr)
        {
            // valid
            foreach (var item in param_constr)
            {
                // lança exceção caso o obj passado para o construtor não exista como argumento na implementação
                if (implementations_constr.FirstOrDefault(c => c.GetType().Equals(item.GetType())) == null)
                    throw new Exception($"{implementation.GetType()} doesn't contains '{item.GetType()}' in your constructor");
            }
        }

        private object GetImplementation(Type component)
        {
            object implementation;
            if (!instances.TryGetValue(component, out implementation))
            {
                implementation = _kernel.Get(component);
                instances.Add(component, implementation);
            }
            return implementation;
        }

        private ConstructorInfo SelectConstructor(Type implementation)
        {
            return implementation.GetConstructors().OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
        }
    }
}