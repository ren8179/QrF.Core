using AutoMapper;
using QrF.Core.Config.Domain;
using QrF.Core.Config.Dto;

namespace QrF.Core.Config.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClientsDto, Clients>();
            CreateMap<ReRoute, ReRouteAccDto>();
        }
    }
}
