using System.Data.Common;

namespace CommandDispatcher
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query, DbTransaction transaction = null);
    }
}