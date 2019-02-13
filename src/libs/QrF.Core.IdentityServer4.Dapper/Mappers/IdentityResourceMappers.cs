using AutoMapper;
using QrF.Core.IdentityServer4.Dapper.Entities;
using Ids4 = IdentityServer4.Models;

namespace QrF.Core.IdentityServer4.Dapper.Mappers
{
    public static class IdentityResourceMappers
    {
        static IdentityResourceMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<IdentityResourceMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Ids4.IdentityResource ToModel(this IdentityResource entity)
        {
            return entity == null ? null : Mapper.Map<Ids4.IdentityResource>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static IdentityResource ToEntity(this Ids4.IdentityResource model)
        {
            return model == null ? null : Mapper.Map<IdentityResource>(model);
        }
    }
}
