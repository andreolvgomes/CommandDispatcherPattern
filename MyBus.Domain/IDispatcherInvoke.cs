using CommandDispatcher.Interfaces;

namespace CommandDispatcher
{
    public interface IDispatcherInvoke : IExecuter, IEventer, IQuerier, ICommander
    {
    }
}