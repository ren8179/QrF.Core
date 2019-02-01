using Microsoft.AspNetCore.Mvc;
using QrF.Core.ComFr.Extension;
using QrF.Core.ComFr.Modules.User.Models;
using QrF.Core.ComFr.Modules.User.Service;
using QrF.Core.ComFr.Mvc.Authorize;
using QrF.Core.ComFr.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QrF.Core.Utils.Extension;
using QrF.Core.ComFr.Constant;

namespace QrF.Core.CMS.Controllers
{
    //[DefaultAuthorize]
    [Route("CMSAPI/[controller]")]
    public class UserController : BasicController<UserEntity, string, IUserService>
    {
        private IApplicationContextAccessor _applicationContextAccessor;
        public UserController(IUserService userService, IApplicationContextAccessor applicationContextAccessor)
            : base(userService)
        {
            _applicationContextAccessor = applicationContextAccessor;
        }

        [HttpGet("GetUserInfo")]
        public IActionResult GetUserInfo(string token)
        {
            var user = Service.Get().FirstOrDefault(o => o.ApiLoginToken == token);
            if (user == null) return BadRequest("无效的token");
            var acesstoken= (user.UserID + user.PassWord + DateTime.Now.ToShortDateString()).ToMd5().ToLower();
            if(user.ApiLoginToken!= acesstoken) return BadRequest("token已失效");
            return Ok(user);
        }
        [HttpPost("Create")]
        public override IActionResult Create([FromBody]UserEntity entity)
        {
            entity.PhotoUrl = Request.SaveImage();
            return base.Create(entity);
        }
        [HttpPost("Edit")]
        public override IActionResult Edit([FromBody]UserEntity entity)
        {
            var url = Request.SaveImage();
            if (!url.IsNullOrWhiteSpace())
            {
                entity.PhotoUrl = url;
            }
            return base.Edit(entity);
        }
        [HttpPost("PassWord")]
        public IActionResult PassWord([FromBody]UserEntity user)
        {
            var logOnUser = Service.Login(_applicationContextAccessor.Current.CurrentUser.UserID, user.PassWord, UserType.Administrator, Request.HttpContext.Connection.RemoteIpAddress.ToString());
            if (logOnUser != null)
            {
                logOnUser.PassWordNew = user.PassWordNew;
                Service.Update(logOnUser);
                return Ok("操作成功");
            }
            return BadRequest("原密码错误");
        }
    }
}
