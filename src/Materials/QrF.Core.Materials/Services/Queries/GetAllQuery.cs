using QrF.Core.Infrastructure.Cqrs.Queries;
using QrF.Core.Materials.Services.Dtos;
using System.Collections.Generic;

namespace QrF.Core.Materials.Services.Queries
{
    public class GetAllQuery : IQuery<IEnumerable<MaterialDto>> { }
}
