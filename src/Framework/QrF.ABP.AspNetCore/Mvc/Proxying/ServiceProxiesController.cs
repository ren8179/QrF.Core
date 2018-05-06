using Microsoft.AspNetCore.Mvc;
using QrF.ABP.AspNetCore.Mvc.Controllers;
using QrF.ABP.Web.Api.ProxyScripting;
using QrF.ABP.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.AspNetCore.Mvc.Proxying
{
    [DontWrapResult]
    public class ServiceProxiesController : BaseController
    {
        private readonly IApiProxyScriptManager _proxyScriptManager;

        public ServiceProxiesController(IApiProxyScriptManager proxyScriptManager)
        {
            _proxyScriptManager = proxyScriptManager;
        }

        [Produces("application/x-javascript")]
        public ContentResult GetAll(ApiProxyGenerationModel model)
        {
            var script = _proxyScriptManager.GetScript(model.CreateOptions());
            return Content(script, "application/x-javascript");
        }
    }
}
