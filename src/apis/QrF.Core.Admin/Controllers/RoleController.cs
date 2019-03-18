using Microsoft.AspNetCore.Mvc;
using QrF.Core.Admin.Dto;
using QrF.Core.Admin.Interfaces;
using System;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [Route("AdminAPI/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleBusiness _business;
        public RoleController(IRoleBusiness business)
        {
            _business = business;
        }
        /// <summary>
        /// 查询分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetPageList")]
        public async Task<BasePageQueryOutput<QueryRoleDto>> GetPageListAsync([FromQuery] QueryRolesInput input)
        {
            return await _business.GetPageList(input);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> EditAsync([FromBody] RoleDto input)
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
            return Ok(new MsgResultDto { Success = true });
        }

    }
}
