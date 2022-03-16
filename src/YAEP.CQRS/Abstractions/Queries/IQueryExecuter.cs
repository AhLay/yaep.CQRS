namespace YAEP.CQRS.Abstractions.Queries
{
    public interface IQueryExecuter
    {
        Task<TResult> Fetch<TResult>(IQuery<TResult> query, CancellationToken cancellationToken);
    }
}
