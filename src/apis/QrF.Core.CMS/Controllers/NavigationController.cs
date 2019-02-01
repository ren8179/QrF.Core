using Microsoft.AspNetCore.Mvc;
using QrF.Core.CMS.Entities;
using QrF.Core.CMS.Service;
using QrF.Core.ComFr.Mvc.Controllers;
using System.Linq;
using QrF.Core.ComFr.ViewPort.ElementUI;

namespace QrF.Core.CMS.Controllers
{
    [Route("CMSAPI/[controller]")]
    public class NavigationController : BasicController<NavigationEntity, string, INavigationService>
    {
        public NavigationController(INavigationService service)
            : base(service) {}

        [HttpGet("GetNavTree")]
        public IActionResult GetNavTree()
        {
            var navs = Service.Get().OrderBy(m => m.DisplayOrder);
            var node = new Tree<NavigationEntity>().Source(navs).ToNode(m => m.ID, m => m.Title, m => m.ParentId, "#");
            return Ok(node);
        }

        [HttpPost("Create")]
        public override IActionResult Create([FromBody]NavigationEntity entity)
        {
            return base.Create(entity);
        }
        [HttpPost("Edit")]
        public override IActionResult Edit([FromBody]NavigationEntity entity)
        {
            return base.Edit(entity);
        }
        
        [HttpPost("MoveNav")]
        public IActionResult MoveNav(string id, string parentId, int position, int oldPosition)
        {
            Service.Move(id, parentId, position, oldPosition);
            return Ok(true);
        }
    }
}
