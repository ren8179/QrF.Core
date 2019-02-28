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
    public class AdvListController : BasicController<AdvListEntity, int, IAdvListService>
    {
        public AdvListController(IAdvListService service)
            : base(service)
        {

        }
        [HttpPost("Create")]
        public override IActionResult Create([FromBody]AdvListEntity entity)
        {
            var result = base.Create(entity);
            return result;
        }
        [HttpPost("Edit")]
        public override IActionResult Edit([FromBody]AdvListEntity entity)
        {
            var result = base.Edit(entity);
            return result;
        }
    }
}
