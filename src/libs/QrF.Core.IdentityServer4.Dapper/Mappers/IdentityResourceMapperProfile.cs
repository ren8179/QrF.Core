﻿using AutoMapper;
using Ids4 = IdentityServer4.Models;

namespace QrF.Core.IdentityServer4.Dapper.Mappers
{
    public class IdentityResourceMapperProfile : Profile
    {
        /// <summary>
        /// <see cref="IdentityResourceMapperProfile"/>
        /// </summary>
        public IdentityResourceMapperProfile()
        {
            CreateMap<Entities.IdentityResource, Ids4.IdentityResource>(MemberList.Destination)
                .ConstructUsing(src => new Ids4.IdentityResource())
                .ReverseMap();

            CreateMap<Entities.IdentityClaim, string>()
               .ConstructUsing(x => x.Type)
               .ReverseMap()
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src));
        }
    }
}
