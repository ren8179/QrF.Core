using AutoMapper;
using QrF.Core.Infrastructure.Cqrs.Queries;
using QrF.Core.Materials.Domain;
using QrF.Core.Materials.Services.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QrF.Core.Materials.Services.Queries
{
    internal class GetAllQueryHandler : IQueryHandler<GetAllQuery, IEnumerable<MaterialDto>>
    {
        private readonly IMaterialStorage _storage;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IMaterialStorage storage, IMapper mapper) 
        {
            _storage = storage;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaterialDto>> ExecuteAsync(GetAllQuery query)
        {
            IEnumerable<Material> list = await _storage.GetAllAsync();
            return _mapper.Map<IEnumerable<MaterialDto>>(list);
        }
    }
}
