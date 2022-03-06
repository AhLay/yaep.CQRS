namespace YAEP.CQRS.Abstractions.Queries
{
    public interface IQueryExecuter
    {
        Task<TResult> Fetch<TQuery,TResult>(TQuery query, CancellationToken cancellationToken)
            where TQuery : IQuery<TResult>;
    }
}
