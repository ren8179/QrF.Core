using QrF.ABP.Web.Api.Modeling;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Web.Api.ProxyScripting.Generators
{
    public interface IProxyScriptGenerator
    {
        string CreateScript(ApplicationApiDescriptionModel model);
    }
}
