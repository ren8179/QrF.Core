using QrF.ABP.Reflection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QrF.ABP.AspNetCore.Configuration
{
    public class ControllerAssemblySettingList : List<ControllerAssemblySetting>
    {
        public ControllerAssemblySetting GetSettingOrNull(Type controllerType)
        {
            return this.FirstOrDefault(controllerSetting => controllerSetting.Assembly == controllerType.GetAssembly());
        }
    }
}
