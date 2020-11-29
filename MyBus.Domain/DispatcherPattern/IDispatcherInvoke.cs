using CommandDispatcher.DispatcherPattern.Interfaces;

namespace CommandDispatcher.DispatcherPattern
{
    public interface IDispatcherInvoke : IExecuter, IEventer, IQuerier, ICommander
    {
    }
}