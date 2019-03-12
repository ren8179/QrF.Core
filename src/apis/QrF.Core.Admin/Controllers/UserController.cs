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
    /// 用户管理
    /// </summary>
    [Route("AdminAPI/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _business;
        public UserController(IUserBusiness business)
        {
            _business = business;
        }
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("QueryUsers")]
        public async Task<BasePageQueryOutput<QueryUserDto>> QueryUsersAsync([FromQuery] QueryUsersInput input)
        {
            return await _business.GetPageList(input);
        }
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("EditUser")]
        public async Task EditUserAsync([FromBody] UserDto input)
        {
            await _business.EditUser(input);
        }
    }
}
