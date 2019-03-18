using AutoMapper;
using QrF.Core.Admin.Domain;
using QrF.Core.Admin.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<RoleDto, Role>();
            CreateMap<OrgDto, Organize>();
        }
    }
}
