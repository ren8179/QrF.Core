using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.AspNetCore.Configuration
{
    public class ControllerAssemblySettingBuilder : IControllerAssemblySettingBuilder
    {
        private readonly ControllerAssemblySetting _setting;

        public ControllerAssemblySettingBuilder(ControllerAssemblySetting setting)
        {
            _setting = setting;
        }

        public ControllerAssemblySettingBuilder Where(Func<Type, bool> predicate)
        {
            _setting.TypePredicate = predicate;
            return this;
        }

        public ControllerAssemblySettingBuilder ConfigureControllerModel(Action<ControllerModel> configurer)
        {
            _setting.ControllerModelConfigurer = configurer;
            return this;
        }
    }
}
