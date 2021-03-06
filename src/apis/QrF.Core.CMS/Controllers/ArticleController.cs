﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("View")]
        public IActionResult View(int id)
        {
            var entity = Service.Get(id);
            entity.Counter = (entity.Counter ?? 0) + 1;
            Service.Update(entity);
            return Ok(entity.Counter);
        }
        [HttpGet("GetList")]
        public IActionResult GetList(int typeId)
        {
            var entities = Service.Get(o => o.ArticleTypeID == typeId && o.IsPublish).OrderByDescending(o => o.PublishDate);
            return Ok(entities);
        }
    }
}
