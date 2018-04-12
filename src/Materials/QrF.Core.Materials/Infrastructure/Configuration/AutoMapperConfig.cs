using AutoMapper;
using QrF.Core.Materials.Domain;
using QrF.Core.Materials.Services.Commands;
using QrF.Core.Materials.Services.Dtos;

namespace QrF.Core.Materials.Infrastructure.Configuration
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Material, MaterialDto>();
                cfg.CreateMap<CreateMaterialCommand, Material>();
            }).CreateMapper();
    }
}
