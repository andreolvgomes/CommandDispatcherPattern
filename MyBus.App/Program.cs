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
            IoCServiceCollection.Ins.Init();
            IoCContainer.Ins.Init();

            IDispatcher dispatcher = IoCContainer.Ins.Get<IDispatcher>();

            dispatcher.Command(new CreateNewCommand());
            dispatcher.Event(new CreateNewEvent());
            dispatcher.Query<string>(new GetProductQuery());
        }
    }
}