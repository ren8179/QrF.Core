using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Web.Api.ProxyScripting
{
    public interface IApiProxyScriptManager
    {
        string GetScript(ApiProxyGenerationOptions options);
    }
}
