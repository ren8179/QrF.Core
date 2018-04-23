using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.AutoMapper
{
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        public List<Action<IMapperConfigurationExpression>> Configurators { get; }

        public bool UseStaticMapper { get; set; }

        public AutoMapperConfiguration()
        {
            UseStaticMapper = true;
            Configurators = new List<Action<IMapperConfigurationExpression>>();
        }
    }
}
