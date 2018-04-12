using QrF.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QrF.Core.Materials.Domain
{
   public interface IMaterialStorage : IStorage
    {
        Task<IEnumerable<Material>> GetAllAsync();
        Task<Material> GetAsyncFor(Guid id);
        Task<Material> GetOrFailAsync(Guid id);
        Task AddAsync(Material user);
    }
}
