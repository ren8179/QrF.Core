using QrF.Core.Cqrs.Domain.Entities;
using System.Threading.Tasks;

namespace QrF.Core.Cqrs.Domain
{
    public interface IRepository<T, TKey> where T : IAggregateRoot
    {
        /// <summary>
        /// Saves the specified aggregate asynchronous.
        /// </summary>
        /// <param name="aggregate">The aggregate.</param>
        /// <returns></returns>
        Task SaveAsync(T aggregate);

        /// <summary>
        /// Saves the specified aggregate.
        /// </summary>
        /// <param name="aggregate">The aggregate.</param>
        void Save(T aggregate);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(TKey id);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T GetById(TKey id);
    }
}
