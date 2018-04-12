using AutoMapper;
using QrF.Core.Infrastructure.Cqrs.Queries;
using QrF.Core.Materials.Domain;
using QrF.Core.Materials.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QrF.Core.Materials.Services.Queries
{
    public class GetForIdQueryHandler : IQueryHandler<GetForIdQuery, MaterialDto>
    {
        private readonly IMaterialStorage _storage;
        private readonly IMapper _mapper;

        public GetForIdQueryHandler(IMaterialStorage storage, IMapper mapper)
        {
            _storage = storage;
            _mapper = mapper;
        }

        public async Task<MaterialDto> ExecuteAsync(GetForIdQuery query)
        {
            var model = await _storage.GetOrFailAsync(query.Id);
            return _mapper.Map<MaterialDto>(model);
        }
    }
}
