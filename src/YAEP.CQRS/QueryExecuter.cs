using YAEP.CQRS.Abstractions;
using YAEP.CQRS.Abstractions.Queries;

namespace YAEP.CQRS
{
    public sealed class QueryExecuter : IQueryExecuter
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryExecuter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.GuardAgainstNull(nameof(serviceProvider));
        }
        public Task<TResult> Fetch<TQuery,TResult>(TQuery query, CancellationToken cancellationToken)
            where TQuery : IQuery<TResult>
        {
            
            var handler = _serviceProvider.GetService(typeof(IQueryHandler<TQuery,TResult>)) as IQueryHandler<TQuery,TResult>;
            handler.GuardAgainst(v => v.IsNull(), $"No Handler found for the query : [{query.GetType()}]. make sure you registred the CommandHandler");

            return handler.Handle(query, cancellationToken);
        }
    }
}
