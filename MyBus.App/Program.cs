using System;
using CommandDispatcher;
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
            Kernel_ArgumentsConstructor.Teste();
            Kernel_Scolpe.Test();
            Kernel_TwoConnection.Test();

            IoCKernel.Ins.Init();
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