using Microsoft.AspNetCore.Mvc;
using QrF.Core.CMS.Entities;
using QrF.Core.CMS.Service;
using QrF.Core.ComFr.Mvc.Controllers;
using QrF.Core.ComFr.ViewPort.ElementUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.CMS.Controllers
{
    [Route("CMSAPI/[controller]")]
    public class ArticleTypeController : BasicController<ArticleType, int, IArticleTypeService>
    {
        public ArticleTypeController(IArticleTypeService service)
            : base(service)
        {
        }
        [HttpPost("Create")]
        public override IActionResult Create([FromBody]ArticleType entity)
        {
            return base.Create(entity);
        }
        [HttpPost("Edit")]
        public override IActionResult Edit([FromBody]ArticleType entity)
        {
            return base.Edit(entity);
        }
        [HttpGet("GetArticleTypeTree")]
        public IActionResult GetArticleTypeTree()
        {
            var allNodes = Service.Get();
            var node = new Tree<ArticleType>().Source(allNodes).ToNode(m => m.ID.ToString(), m => m.Title, m => m.ParentID.ToString(),m =>0, "0");
            return Ok(node);
        }

    }
}
