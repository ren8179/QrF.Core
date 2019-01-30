using Microsoft.AspNetCore.Mvc;
using QrF.Core.Utils.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.CMS.Controllers
{
    /// <summary>
    /// 服务状态监测
    /// </summary>
    [Produces("application/json")]
    [Route("CMSAPI/Health")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// 检查服务状态
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get() => Ok("ok");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Info")]
        public ActionResult<IEnumerable<string>> Info()
        {
            return new string[] {
                $"TicketsAPI => {NetworkHelper.LocalIPAddress}",
                $"TicketsAPI: {DateTime.Now.ToString()} {Environment.MachineName} OS: {Environment.OSVersion.VersionString}"
            };
        }
    }
}
