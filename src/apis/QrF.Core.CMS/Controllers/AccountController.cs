using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QrF.Core.ComFr.Constant;
using QrF.Core.ComFr.Modules.User.Service;
using QrF.Core.ComFr.Mvc.Authorize;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QrF.Core.CMS.Controllers
{
    [Route("CMSAPI/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserService userService,
            ILogger<AccountController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(string userName, string password)
        {
            var user = _userService.Login(userName, password, UserType.Administrator, Request.HttpContext.Connection.RemoteIpAddress.ToString());
            if (user == null)
                return Json(new { isSuccess = false, msg = "登录失败，用户名密码不正确" });
            user.AuthenticationType = DefaultAuthorizeAttribute.DefaultAuthenticationScheme;
            var identity = new ClaimsIdentity(user);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserID));
            await HttpContext.SignInAsync(DefaultAuthorizeAttribute.DefaultAuthenticationScheme, new ClaimsPrincipal(identity));
            return Json(new { isSuccess = true, msg = "登录成功", token = user.ApiLoginToken });
        }

        [HttpGet("Logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(DefaultAuthorizeAttribute.DefaultAuthenticationScheme);
        }
    }
}
