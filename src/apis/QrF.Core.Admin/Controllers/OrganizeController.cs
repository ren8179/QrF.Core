using Microsoft.AspNetCore.Mvc;
using QrF.Core.Admin.Domain;
using QrF.Core.Admin.Dto;
using QrF.Core.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("AdminAPI/[controller]")]
    [ApiController]
    public class OrganizeController : ControllerBase
    {
        private readonly IOrganizeBusiness _business;
        public OrganizeController(IOrganizeBusiness business)
        {
            _business = business;
        }
        /// <summary>
        /// 查询分页列表
        /// </summary>
        [HttpGet("GetPageList")]
        public async Task<BasePageQueryOutput<QueryOrganizeDto>> GetPageListAsync([FromQuery] QueryOrganizesInput input)
        {
            return await _business.GetPageList(input);
        }

        /// <summary>
        /// 获取模块级联列表
        /// </summary>
        [HttpGet("GetCascaderList")]
        public async Task<IEnumerable<CascaderItem>> GetCascaderListAsync()
        {
            var list = await _business.GetListAsync(null);
            if (list.Count(o => o.ParentId == null || o.ParentId == Guid.Empty) < 1)
                return null;
            var result = from d in list.Where(o => o.ParentId == null || o.ParentId == Guid.Empty)
                         select new CascaderItem
                         {
                             value = d.KeyId,
                             label = d.Name,
                             children = GetCascaderItem(d.KeyId, list)
                         };
            return result;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpPost("Edit")]
        public async Task<IActionResult> EditAsync([FromBody] OrgDto input)
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
            if (input == null || !input.KeyId.HasValue) throw new Exception("编号不存在");
            await _business.DelModel(input.KeyId.Value);
            return Ok(new MsgResultDto { Success=true });
        }

        private IEnumerable<CascaderItem> GetCascaderItem(Guid pid, IEnumerable<QueryOrganizeDto> all)
        {
            if (all.Count(o => o.ParentId == pid) < 1)
                return null;
            var result = from d in all.Where(o => o.ParentId == pid)
                         select new CascaderItem
                         {
                             value = d.KeyId,
                             label = d.Name,
                             children = GetCascaderItem(d.KeyId, all)
                         };
            return result;
        }

    }
}
