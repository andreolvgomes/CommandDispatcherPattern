using MessagerBus;
using My.Tests.Events;
using My.Tests.Queries;
using MyBus.Tests.Commands;

namespace MyBus.App
{
    public class Program
    {
        public static void Main(string[] args)
        {          
            IoC.Ins.Init();

            IDispatcher dispatcher = IoC.Ins.Get<IDispatcher>();

            dispatcher.Command(new CreateNewCommand());
            dispatcher.Event(new CreateNewEvent());
            dispatcher.Query<string>(new GetProductQuery());
        }
    }
}