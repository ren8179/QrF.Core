using AutoMapper;
using Ids4 = IdentityServer4.Models;

namespace QrF.Core.IdentityServer4.Dapper.Mappers
{
    public class PersistedGrantMapperProfile : Profile
    {
        /// <summary>
        /// <see cref="PersistedGrantMapperProfile">
        /// </see>
        /// </summary>
        public PersistedGrantMapperProfile()
        {
            CreateMap<Entities.PersistedGrant, Ids4.PersistedGrant>(MemberList.Destination)
                .ReverseMap();
        }
    }
}
