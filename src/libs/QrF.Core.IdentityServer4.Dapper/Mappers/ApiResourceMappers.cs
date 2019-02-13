﻿using AutoMapper;
using QrF.Core.IdentityServer4.Dapper.Entities;
using Ids4 = IdentityServer4.Models;

namespace QrF.Core.IdentityServer4.Dapper.Mappers
{
    public static class ApiResourceMappers
    {
        static ApiResourceMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ApiResourceMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Ids4.ApiResource ToModel(this ApiResource entity)
        {
            return entity == null ? null : Mapper.Map<Ids4.ApiResource>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static ApiResource ToEntity(this Ids4.ApiResource model)
        {
            return model == null ? null : Mapper.Map<ApiResource>(model);
        }
    }
}
