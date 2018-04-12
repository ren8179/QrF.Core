using QrF.Core.Infrastructure.Cqrs.Queries;
using QrF.Core.Materials.Services.Dtos;
using System;

namespace QrF.Core.Materials.Services.Queries
{
    public class GetForIdQuery : IQuery<MaterialDto>
    {
        public Guid Id { get; }

        public GetForIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
