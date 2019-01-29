using Microsoft.AspNetCore.Mvc;
using QrF.Core.CMS.Entities;
using QrF.Core.CMS.Service;
using QrF.Core.ComFr.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.CMS.Controllers
{
    public class NavigationController : BasicController<NavigationEntity, string, INavigationService>
    {
        public NavigationController(INavigationService service)
            : base(service)
        {

        }
        
        public IActionResult Create(string ParentID)
        {
            var navication = new NavigationEntity
            {
                ParentId = ParentID,
                DisplayOrder = Service.Count(m => m.ParentId == ParentID) + 1
            };
            return View(navication);
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
        public JsonResult MoveNav(string id, string parentId, int position, int oldPosition)
        {
            Service.Move(id, parentId, position, oldPosition);
            return Json(true);
        }
    }
}
