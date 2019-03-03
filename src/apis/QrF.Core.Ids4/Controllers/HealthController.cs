using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Ids4.Controllers
{
    /// <summary>
    /// 服务状态监测
    /// </summary>
    [Produces("application/json")]
    [Route("Ids4API/[controller]")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// 检查服务状态
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get() => Ok("ok");
    }
}
