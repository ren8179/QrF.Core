using Autofac;
using System;
using System.Threading.Tasks;

namespace QrF.Core.Infrastructure.Cqrs.Queries
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly IComponentContext _context;

        public QueryExecutor(IComponentContext context)
        {
            _context = context;
        }

        public async Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = GetHandler<IQueryHandler<TQuery, TResult>, TQuery>(query);
            return await handler.ExecuteAsync((dynamic)query);
        }
        private THandler GetHandler<THandler, TQuery>(TQuery query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query), $"Query: {query.GetType().Name} cannot be null.");

            var queryHandler = _context.Resolve<THandler>();

            if (queryHandler == null)
                throw new Exception($"No handler found for query '{query.GetType().FullName}'");

            return queryHandler;
        }
    }
}
