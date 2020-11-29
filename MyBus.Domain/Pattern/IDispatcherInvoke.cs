using CommandDispatcher.Pattern.Interfaces;

namespace CommandDispatcher.Pattern
{
    public interface IDispatcherInvoke : IExecuter, IEventer, IQuerier, ICommander
    {
    }
}