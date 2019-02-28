using Microsoft.AspNetCore.Mvc;
using QrF.Core.CMS.Entities;
using QrF.Core.CMS.Service;
using QrF.Core.ComFr.Constant;
using QrF.Core.ComFr.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.CMS.Controllers
{
    [Route("CMSAPI/[controller]")]
    public class AdvClassController : BasicController<AdvClassEntity, int, IAdvClassService>
    {
        public AdvClassController(IAdvClassService service)
            : base(service)
        {

        }
        [HttpPost("Create")]
        public override IActionResult Create([FromBody]AdvClassEntity entity)
        {
            var result = base.Create(entity);
            return result;
        }
        [HttpPost("Edit")]
        public override IActionResult Edit([FromBody]AdvClassEntity entity)
        {
            var result = base.Edit(entity);
            return result;
        }
        [HttpGet("GetList")]
        public virtual IActionResult GetList()
        {
            return Ok(Service.Get());
        }
    }
}
