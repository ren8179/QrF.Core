using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Materials.Domain
{
    internal class InMemoryMaterialStorage : IMaterialStorage
    {
        private readonly ISet<Material> _list = new HashSet<Material>();

        public async Task AddAsync(Material model)
        {
            _list.Add(model);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Material>> GetAllAsync()
            => await Task.FromResult(_list);

        public async Task<Material> GetAsyncFor(string name)
            => await Task.FromResult(_list.SingleOrDefault(u => u.Name == name));

        public async Task<Material> GetAsyncFor(Guid id)
            => await Task.FromResult(_list.SingleOrDefault(u => u.Id == id));

        public async Task<Material> GetOrFailAsync(Guid id)
            => await Task.FromResult(_list.SingleOrDefault(u => u.Id == id));
    }
}
