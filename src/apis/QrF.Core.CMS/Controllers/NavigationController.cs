using Microsoft.AspNetCore.Mvc;
using QrF.Core.CMS.Entities;
using QrF.Core.CMS.Service;
using QrF.Core.ComFr.Mvc.Controllers;

namespace QrF.Core.CMS.Controllers
{
    [Route("CMSAPI/[controller]")]
    public class NavigationController : BasicController<NavigationEntity, string, INavigationService>
    {
        public NavigationController(INavigationService service)
            : base(service) {}

        /// <summary>
        /// 检查服务状态
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get() => Ok("NavigationController");
        public IActionResult Create(string ParentID)
        {
            var navication = new NavigationEntity
            {
                ParentId = ParentID,
                DisplayOrder = Service.Count(m => m.ParentId == ParentID) + 1
            };
            return Ok(navication);
        }
        [HttpPost]
        public override IActionResult Create(NavigationEntity entity)
        {
            return base.Create(entity);
        }
        [HttpPost]
        public override IActionResult Edit(NavigationEntity entity)
        {
            return base.Edit(entity);
        }
        
        [HttpPost]
        public IActionResult MoveNav(string id, string parentId, int position, int oldPosition)
        {
            Service.Move(id, parentId, position, oldPosition);
            return Ok(true);
        }
    }
}
