using Autofac;
using System;
using System.Threading.Tasks;

namespace QrF.Core.Infrastructure.Cqrs.Queries
{
    internal class QueryExecutor : IQueryExecutor
    {
        private readonly IComponentContext _context;

        public QueryExecutor(IComponentContext context)
        {
            _context = context;
        }

        public async Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query), $"Query: {query.GetType().Name} cannot be null.");
            }
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _context.Resolve(handlerType);
            return await handler.ExecuteAsync((dynamic)query);
        }
    }
}
