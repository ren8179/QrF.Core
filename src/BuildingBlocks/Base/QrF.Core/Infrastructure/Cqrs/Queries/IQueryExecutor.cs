using System.Threading.Tasks;

namespace QrF.Core.Infrastructure.Cqrs.Queries
{
    public interface IQueryExecutor
    {
        Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query);
    }
}
