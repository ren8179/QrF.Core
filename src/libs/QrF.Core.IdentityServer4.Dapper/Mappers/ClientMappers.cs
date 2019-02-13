using AutoMapper;
using Ids4 = IdentityServer4.Models;

namespace QrF.Core.IdentityServer4.Dapper.Mappers
{
    public static class ClientMappers
    {
        static ClientMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ClientMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static Ids4.Client ToModel(this Entities.Client entity)
        {
            return Mapper.Map<Ids4.Client>(entity);
        }

        public static Entities.Client ToEntity(this Ids4.Client model)
        {
            return Mapper.Map<Entities.Client>(model);
        }
    }
}
