using Microsoft.AspNetCore.Mvc;
using QrF.Core.Config.Domain;
using QrF.Core.Config.Dto;
using QrF.Core.Config.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("ConfigAPI/[controller]")]
    [ApiController]
    public class ReRouteController : ControllerBase
    {
        private readonly IReRouteBusiness _business;
        private readonly IReRoutesItemBusiness _itemBusiness;
        public ReRouteController(IReRouteBusiness business, IReRoutesItemBusiness itemBusiness)
        {
            _business = business;
            _itemBusiness = itemBusiness;
        }

        /// <summary>
        /// 获取分类级联列表
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("GetCascaderList")]
        public async Task<IEnumerable<CascaderItem>> GetCascaderListAsync()
        {
            var list = await _itemBusiness.GetAllList();
            if (list.Count(o => o.ItemParentId == null || o.ItemParentId == 0) < 1)
                return null;
            var result = from d in list.Where(o => o.ItemParentId == null || o.ItemParentId == 0)
                         select new CascaderItem
                         {
                             value = d.ItemId,
                             label = d.ItemName,
                             children = GetCascaderItem(d.ItemId, list)
                         };
            return result;
        }
        /// <summary>
        /// 编辑
        /// </summary>
        [HttpPost("EditType")]
        public async Task<IActionResult> EditTypeAsync([FromBody] ReRoutesItem input)
        {
            await _itemBusiness.EditModel(input);
            return Ok(new MsgResultDto { Success = true });
        }
        /// <summary>
        /// 查询分页列表
        /// </summary>
        [HttpGet("GetPageList")]
        public async Task<IActionResult> GetPageListAsync([FromQuery] PageInput input)
        {
            var list = await _business.GetPageList(input);
            return Ok(list);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        [HttpPost("Edit")]
        public async Task<IActionResult> EditAsync([FromBody] ReRoute input)
        {
            await _business.EditModel(input);
            return Ok(new MsgResultDto { Success = true });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] DelInput input)
        {
            if (input == null || !input.Id.HasValue) throw new Exception("编号不存在");
            await _business.DelModel(input.Id.Value);
            return Ok(new MsgResultDto { Success = true });
        }
        
        private IEnumerable<CascaderItem> GetCascaderItem(int pid, IEnumerable<ReRoutesItem> all)
        {
            if (all.Count(o => o.ItemParentId == pid) < 1)
                return null;
            var result = from d in all.Where(o => o.ItemParentId == pid)
                         select new CascaderItem
                         {
                             value = d.ItemId,
                             label = d.ItemName,
                             children = GetCascaderItem(d.ItemId, all)
                         };
            return result;
        }

    }
}
