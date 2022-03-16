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

        public Task<TResult> Fetch<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        {
            //learned from : https://github.com/vkhorikov/CqrsInPractice/blob/master/After/src/Logic/Utils/Messages.cs
            Type type = typeof(IQueryHandler<,>);
            Type[] typeArgs = { query.GetType(), typeof(TResult) };
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = _serviceProvider.GetService(handlerType)
                              ?? throw new InvalidOperationException($"No Handler found for the query : [{query.GetType()}]. make sure you registred the CommandHandler");

            return handler.Handle((dynamic)query, cancellationToken);
        }
    }
}
