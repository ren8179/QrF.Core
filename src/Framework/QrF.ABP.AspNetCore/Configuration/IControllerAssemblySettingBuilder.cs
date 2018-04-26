using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.AspNetCore.Configuration
{
    public interface IControllerAssemblySettingBuilder
    {
        ControllerAssemblySettingBuilder Where(Func<Type, bool> predicate);

        ControllerAssemblySettingBuilder ConfigureControllerModel(Action<ControllerModel> configurer);
    }
}
