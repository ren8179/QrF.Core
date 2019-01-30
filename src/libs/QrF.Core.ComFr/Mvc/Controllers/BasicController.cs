using Microsoft.AspNetCore.Mvc;
using QrF.Core.ComFr.Constant;
using QrF.Core.ComFr.Entities;
using QrF.Core.ComFr.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.Mvc.Controllers
{
    /// <summary>
    /// 基本控制器，增删改查
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimarykey">主键类型</typeparam>
    /// <typeparam name="TService">Service类型</typeparam>
    public class BasicController<TEntity, TPrimarykey, TService> : ControllerBase
        where TEntity : EditorEntity
        where TService : IService<TEntity>
    {
        public TService Service;
        public BasicController(TService service)
        {
            Service = service;
        }
        
        [HttpPost]
        public virtual IActionResult Create(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                var result = Service.Add(entity);
                if (result.HasViolation)
                {
                    foreach (var item in result.RuleViolations)
                    {
                        ModelState.AddModelError(item.ParameterName, item.ErrorMessage);
                    }
                    return Ok(entity);
                }
            }
            return Ok(entity);
        }
        public virtual IActionResult Edit(TPrimarykey Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            TEntity entity = Service.Get(Id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public virtual IActionResult Edit(TEntity entity)
        {
            if (entity.ActionType == ActionType.Delete)
            {
                Service.Remove(entity);
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var result = Service.Update(entity);
                if (result.HasViolation)
                {
                    foreach (var item in result.RuleViolations)
                    {
                        ModelState.AddModelError(item.ParameterName, item.ErrorMessage);
                    }
                    return Ok(entity);
                }
                return RedirectToAction("Index");
            }
            return Ok(entity);
        }

        [HttpPost]
        public virtual IActionResult Delete(TPrimarykey id)
        {
            try
            {
                Service.Remove(id);
                return Ok(new AjaxResult { Status = AjaxStatus.Normal });
            }
            catch (Exception ex)
            {
                return Ok(new AjaxResult { Status = AjaxStatus.Error, Message = ex.Message });
            }
        }
        [HttpPost]
        public virtual IActionResult GetList(DataTableOption query)
        {
            var pagin = new Pagination { PageSize = query.Length, PageIndex = query.Start / query.Length };
            var expression = query.AsExpression<TEntity>();
            var order = query.GetOrderBy<TEntity>();
            if (order != null)
            {
                if (query.IsOrderDescending())
                {
                    pagin.OrderByDescending = order;
                }
                else
                {
                    pagin.OrderBy = order;
                }
            }
            var entities = Service.Get(expression, pagin);
            return Ok(new TableData(entities, pagin.RecordCount, query.Draw));
        }
    }
}
