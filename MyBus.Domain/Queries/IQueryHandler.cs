using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CommandDispatcher
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query, SqlTransaction transaction = null);
    }
}