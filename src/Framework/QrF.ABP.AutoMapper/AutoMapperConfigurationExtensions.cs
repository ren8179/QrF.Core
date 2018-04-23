using AutoMapper;
using QrF.ABP.Configuration.Startup;
using System;
using System.Reflection;

namespace QrF.ABP.AutoMapper
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure Abp.AutoMapper module.
    /// </summary>
    public static class AutoMapperConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Abp.AutoMapper module.
        /// </summary>
        public static IAutoMapperConfiguration AutoMapper(this IModuleConfigurations configurations)
        {
            return configurations.Configuration.Get<IAutoMapperConfiguration>();
        }

        public static void CreateAutoAttributeMaps(this IMapperConfigurationExpression configuration, Type type)
        {
            foreach (var autoMapAttribute in type.GetTypeInfo().GetCustomAttributes<AutoMapAttributeBase>())
            {
                autoMapAttribute.CreateMap(configuration, type);
            }
        }
    }
}
