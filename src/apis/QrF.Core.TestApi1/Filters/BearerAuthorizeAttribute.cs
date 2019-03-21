using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QrF.Core.TestApi1.Filters
{
    public class BearerAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public const string DefaultAuthenticationScheme = "Bearer";
        /// <summary>
        /// 认证服务器地址
        /// </summary>
        private string issUrl = "";
        /// <summary>
        /// 保护的API名称
        /// </summary>
        private string apiName = "";
        private readonly ILogger _log = Log.ForContext<BearerAuthorizeAttribute>();
        public BearerAuthorizeAttribute(string IssUrl, string ApiName)
        {
            issUrl = IssUrl;
            apiName = ApiName;
            this.AuthenticationSchemes = DefaultAuthenticationScheme;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }
            var currentuser = context.HttpContext.User;
            var result = await context.HttpContext.AuthenticateAsync(DefaultAuthenticationScheme);
            if (result == null || !result.Succeeded)
            {
                string authHeader = context.HttpContext.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Bearer "))
                {
                    try
                    {
                        var httpclient = new HttpClient();
                        var jwtKey = await httpclient.GetStringAsync(issUrl + "/.well-known/openid-configuration/jwks");
                        var Ids4keys = JsonConvert.DeserializeObject<Ids4Keys>(jwtKey);
                        var jwk = Ids4keys.keys;
                        var handler = new JwtSecurityTokenHandler();
                        var claims = handler.ValidateToken(authHeader.Replace("Bearer ", ""), new TokenValidationParameters
                        {
                            ValidIssuer = issUrl,
                            IssuerSigningKeys = jwk,
                            ValidateLifetime = true,
                            ValidAudience = apiName
                        }, out var _);
                        context.HttpContext.User = claims;
                        await context.HttpContext.SignInAsync(DefaultAuthenticationScheme, claims);
                        return;
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex, "验证授权失败：{apiName} {issUrl}", apiName, issUrl);
                        context.Result = new UnauthorizedResult();
                    }
                }
                context.Result = new UnauthorizedResult();
            }
            return;
        }
    }
    public class Ids4Keys
    {
        public JsonWebKey[] keys { get; set; }
    }
}
