using Microsoft.AspNetCore.Mvc;
using QrF.Core.Admin.Dto;
using QrF.Core.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IPermissionsBusiness _perBusiness;
        private readonly IOrganizeBusiness _orgBusiness;
        public RoleController(IRoleBusiness business, IPermissionsBusiness perBusiness,
            IOrganizeBusiness orgBusiness)
        {
            _business = business;
            _perBusiness = perBusiness;
            _orgBusiness = orgBusiness;
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
        /// 分页列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserRolePageList")]
        public async Task<IActionResult> GetUserRolePageListAsync([FromQuery] QueryUserRolesInput input)
        {
            var res = await _business.GetPageList(input);
            var pers = await _perBusiness.GetListByUserTypeAsync(input.UserId);
            foreach (var item in res.Rows)
            {
                var dept = await _orgBusiness.GetModelAsync(item.DeptId);
                item.DeptName = dept?.Name;
                item.IsAuth = pers.Count(o => o.RoleId == item.KeyId) > 0;
            }
            return Ok(res);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        [HttpPost("Edit")]
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

        /// <summary>
        /// 用户授权角色
        /// </summary>
        /// <returns></returns>
        [HttpPost("ToRole")]
        public async Task<ObjectResult> AdminToRoleAsync([FromBody] ToRoleInput input)
        {
            var result = await _perBusiness.ToRole(input);
            return Ok(result);
        }
        /// <summary>
        /// 获取角色已授权的菜单编号
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("GetAccessMenuIds")]
        public async Task<IActionResult> GetAccessMenuIdsAsync(Guid roleId)
        {
            var list = await _perBusiness.GetListByRoleTypeAsync(roleId);
            if (list != null && list.Count() > 0)
                return Ok(list.Select(o => o.MenuId).Distinct());
            return Ok(list);
        }

        /// <summary>
        /// 角色授权
        /// </summary>
        /// <returns></returns>
        [HttpPost("SaveRoleMenu")]
        public async Task<ObjectResult> SaveRoleMenuAsync([FromBody] RoleMenuDto input)
        {
            var result = await _perBusiness.SaveRoleMenu(input);
            return Ok(result);
        }

    }
}
