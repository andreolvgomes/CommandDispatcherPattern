using System;
using MessagerBus;
using My.Tests.Events;
using My.Tests.Queries;
using MyBus.Tests.Commands;
using Ninject;
using Ninject.Activation.Blocks;

namespace MyBus.App
{
    public static class JJJ
    {
        public static void Add<T>(this StandardKernel bind)
        {
            bind.Bind<T>().To<T>();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            //https://csharp.hotexamples.com/pt/examples/Ninject/StandardKernel/Dispose/php-standardkernel-dispose-method-examples.html
            var kernel = new StandardKernel();
            kernel.Bind<IClassWithDisposable>().To<ClassWithDisposable>();
            //kernel.Bind<Produtos>().ToMethod(ctx => new Produtos());
            kernel.Bind<Produtos>().To<Produtos>();
            //kernel.Add<Produtos>();

            var test = kernel.Get<IClassWithDisposable>();
            var produtos = kernel.Get<Produtos>();
            
            using (IActivationBlock v = kernel.BeginBlock())
            {
                var produtos2 = v.Get<Produtos>();
            }

            IoCServiceCollection.Ins.Init();
            IoCContainer.Ins.Init();

            IDispatcher dispatcher = IoCContainer.Ins.Get<IDispatcher>();

            dispatcher.Command(new CreateNewCommand());
            dispatcher.Event(new CreateNewEvent());
            dispatcher.Query<string>(new GetProductQuery());
        }
    }

    public class Produtos : IDisposable
    {
        public string Str { get; set; }

        public Produtos()
        {
        }

        public void Dispose()
        {

        }
    }
}