using System.Threading.Tasks;

namespace QrF.Core.Infrastructure.Cqrs.Queries
{
    public interface IQueryExecutor
    {
        Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}
