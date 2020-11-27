using System;
using System.Collections.Generic;
using System.Text;
using Ninject;
using Ninject.Activation.Blocks;

namespace MyBus.App
{
    public class Kernel_TwoConnection
    {
        public static void Test()
        {
            //https://csharp.hotexamples.com/pt/examples/Ninject/StandardKernel/Dispose/php-standardkernel-dispose-method-examples.html
            var kernel = new StandardKernel();

            kernel.Bind<IConnectionRemoto>().ToMethod(ctx => new ConnectionRemoto("IConnectionRemoto"));
            kernel.Bind<IConnectionLocal>().ToMethod(ctx => new ConnectionLocal("ConnectionLocal"));
            kernel.Bind<IClassWithDisposable>().To<ClassWithDisposable>();
            kernel.Bind<Produtos>().To<Produtos>();

            IConnectionRemoto remoto = kernel.Get<IConnectionRemoto>();
            IConnectionLocal local = kernel.Get<IConnectionLocal>();

            using (IActivationBlock v = kernel.BeginBlock())
            {
                var produtos2 = v.Get<Produtos>();
            }
        }
    }
}
