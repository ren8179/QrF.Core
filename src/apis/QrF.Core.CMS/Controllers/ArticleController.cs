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
    public class ArticleController : BasicController<ArticleEntity, int, IArticleService>
    {
        public ArticleController(IArticleService service)
            : base(service)
        {
           
        }
        [HttpPost("Create")]
        public override IActionResult Create([FromBody]ArticleEntity entity)
        {
            var result = base.Create(entity);
            if (entity.ActionType == ActionType.Publish)
            {
                Service.Publish(entity.ID);
            }
            return result;
        }
        [HttpPost("Edit")]
        public override IActionResult Edit([FromBody]ArticleEntity entity)
        {
            var result = base.Edit(entity);
            if (entity.ActionType == ActionType.Publish)
            {
                Service.Publish(entity.ID);
            }
            return result;
        }
        [HttpPost("GetList")]
        public override IActionResult GetList([FromBody]DataTableOption query)
        {
            return base.GetList(query);
        }
        [HttpPost("Delete")]
        public override IActionResult Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
