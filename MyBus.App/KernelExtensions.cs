using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Ninject;
using Ninject.Infrastructure;
using Ninject.Planning.Bindings;

namespace MyBus.App
{
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
}