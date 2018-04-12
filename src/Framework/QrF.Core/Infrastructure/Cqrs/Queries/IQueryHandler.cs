using System.Threading.Tasks;

namespace QrF.Core.Infrastructure.Cqrs.Queries
{
    public interface IQueryHandler<T, TResult> where T : IQuery<TResult>
    {
        Task<TResult> ExecuteAsync(T query);
    }
}
