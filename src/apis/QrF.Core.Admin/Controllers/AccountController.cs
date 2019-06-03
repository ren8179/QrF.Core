using Microsoft.AspNetCore.Authorization;
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
    /// 
    /// </summary>
    [Route("AdminAPI/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserBusiness _business;
        public AccountController(IUserBusiness business)
        {
            _business = business;
        }
        /// <summary>
        /// 登录
        /// </summary>
        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(LoginInput input)
        {
            try
            {
                if (input == null || string.IsNullOrEmpty(input.Username) || string.IsNullOrEmpty(input.Password))
                    throw new Exception("用户名或密码不能为空");
                var user = _business.FindUserByuAccount(input.Username, input.Password);
                if (user == null) throw new Exception("用户名或密码错误");
                user.Password = null;
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [HttpGet("GetUserInfo")]
        public IActionResult GetUserInfo(Guid token)
        {
            try
            {
                var user = _business.GetByKeyId(token);
                if (user == null) throw new Exception("用户不存在");
                user.Password = null;
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
