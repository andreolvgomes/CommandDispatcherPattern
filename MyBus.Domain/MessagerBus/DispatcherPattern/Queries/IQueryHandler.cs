namespace MessagerBus
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery command);
    }
}