using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QrF.Core.Admin.Dto;
using QrF.Core.Admin.Interfaces;

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
        /// 查询角色列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("QueryRoles")]
        public async Task<BasePageQueryOutput<QueryRoleDto>> QueryRolesAsync([FromQuery] QueryRolesInput input)
        {
            return await _business.GetPageList(input);
        }
    }
}
