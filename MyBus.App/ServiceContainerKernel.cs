using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using CommandDispatcher;
using Ninject;
using Ninject.Activation.Blocks;
using Ninject.Parameters;

namespace MyBus.App
{
    public class ServiceContainerKernel : IServiceContainer
    {
        private readonly static Dictionary<Type, object> instances = new Dictionary<Type, object>();
        private readonly static Dictionary<Type, object> instancesOfCnn = new Dictionary<Type, object>();

        private readonly IKernel _kernel;

        public ServiceContainerKernel(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Cria nova instância de serviceType
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="params_constructor"></param>
        /// <returns></returns>
        public object GetInstance(Type serviceType, object[] params_constructor = null)
        {
            if (params_constructor != null && params_constructor.Length > 0)
            {
                var arguments = Arguments(serviceType, params_constructor);
                return _kernel.Get(serviceType, arguments.ToArray());
            }

            var argumentsCnn = ArgumentsInstanceOfCnn(serviceType);
            return _kernel.Get(serviceType, argumentsCnn.ToArray());
        }

        /// <summary>
        /// Se a implementação de 'serviceType' possui como argumento no construtor um obj do tipo 'ConnectionDbAbstract', então organiza uma instância única para que
        /// seja repassada a todas os outros argumentos do construtor de 'serviceType'
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        private List<ConstructorArgument> ArgumentsInstanceOfCnn(Type serviceType)
        {
            var implementation = GetImplementationCnn(serviceType);
            var constructor = SelectConstructor(implementation.GetType());
            if (constructor == null) return new List<ConstructorArgument>();
            var arguments = new List<ConstructorArgument>();
            var parameters = constructor.GetParameters();

            foreach (var parameter in parameters)
            {
                if (parameter.ParameterType != typeof(SqlConnection))
                    continue;

                var implemt = _kernel.Get(parameter.ParameterType);
                arguments.Add(new ConstructorArgument(parameter.Name, implemt, shouldInherit: true));
            }
            return arguments;
        }

        /// <summary>
        /// Instância e guarda na memória a instância da implementação de 'component'
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        private object GetImplementationCnn(Type component)
        {
            object implementation;
            if (!instancesOfCnn.TryGetValue(component, out implementation))
            {
                implementation = _kernel.Get(component);
                instancesOfCnn.Add(component, implementation);
            }
            return implementation;
        }

        /// <summary>
        /// Organiza e retorna a lista de argumentos do construtor da implementação de 'component'
        /// </summary>
        /// <param name="component"></param>
        /// <param name="params_constructor"></param>
        /// <returns></returns>
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

                var param_constr = params_constructor.ToList().FirstOrDefault(c => c.GetType().Equals(implemt.GetType()));
                if (param_constr != null)
                    arguments.Add(new ConstructorArgument(parameter.Name, param_constr, shouldInherit: true));
            }

            // valid
            CheckImplementationsThrow(implementations_constr, implementation, params_constructor);

            return arguments;
        }

        /// <summary>
        /// Verifica se o argumento(construtor) personalizado passado por parâmetro é realmente esperado pela implementação
        /// </summary>
        /// <param name="implementations_constr"></param>
        /// <param name="implementation"></param>
        /// <param name="param_constr"></param>
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

        /// <summary>
        /// Instância e guarda na memória a instância da implementação de 'component'
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get list 
        /// </summary>
        /// <param name="implementation"></param>
        /// <returns></returns>
        private ConstructorInfo SelectConstructor(Type implementation)
        {
            return implementation.GetConstructors().OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
        }
    }
}