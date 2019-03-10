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
    /// 服务状态监测
    /// </summary>
    [Route("AdminAPI/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("QueryUsers")]
        public async Task<QueryUsersOutput> QueryUsersAsync([FromQuery] QueryUsersInput input)
        {
            return await _userBusiness.QueryUsers(input);
        }
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("EditUser")]
        public async Task EditUserAsync([FromBody] UserDto input)
        {
            await _userBusiness.EditUser(input);
        }
    }
}
