using My.Tests.Commands;
using MessagerBus;
using MyBus.Tests.Commands;

namespace MyBus.App
{
    public class Program
    {
        public static void Main(string[] args)
        {          
            IoC.Ins.Init();

            IDispatcherBus dispatcher = IoC.Ins.Get<IDispatcherBus>();

            dispatcher.Execute(new DesmebrarProduto());
        }
    }
}