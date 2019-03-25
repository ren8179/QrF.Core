using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using QrF.Core.TestApi1.Configuration;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QrF.Core.TestApi1.Filters
{
    public class BearerAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public const string DefaultAuthenticationScheme = "BearerAuth";
        /// <summary>
        /// 认证服务器地址
        /// </summary>
        private string issUrl = "";
        /// <summary>
        /// 保护的API名称
        /// </summary>
        private string apiName = "";
        private readonly ILogger _log = Log.ForContext<BearerAuthorizeAttribute>();
        private IMemoryCache _cache;
        private readonly AppSettings _options;
        public BearerAuthorizeAttribute(IOptions<AppSettings> optionsAccessor, IMemoryCache memoryCache)
        {
            this.AuthenticationSchemes = DefaultAuthenticationScheme;
            _options = optionsAccessor.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
            _cache = memoryCache;
            issUrl = _options.Auth.ServerUrl;
            apiName = _options.Auth.ApiName;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }
            var result = await context.HttpContext.AuthenticateAsync(DefaultAuthenticationScheme);
            if (result == null || !result.Succeeded)
            {
                string authHeader = context.HttpContext.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Bearer "))
                {
                    try
                    {
                        var token = authHeader.Replace("Bearer ", "");
                        ClaimsPrincipal cacheEntry;
                        if (!_cache.TryGetValue(token, out cacheEntry))
                        {
                            var httpclient = new HttpClient();
                            var jwtKey = await httpclient.GetStringAsync(issUrl + "/.well-known/openid-configuration/jwks");
                            var Ids4keys = JsonConvert.DeserializeObject<Ids4Keys>(jwtKey);
                            var handler = new JwtSecurityTokenHandler();
                            cacheEntry = handler.ValidateToken(token, new TokenValidationParameters
                            {
                                ValidIssuer = issUrl,
                                IssuerSigningKeys = Ids4keys.Keys,
                                ValidateLifetime = true,
                                ValidAudience = apiName
                            }, out var _);
                            _cache.Set(token, cacheEntry, new MemoryCacheEntryOptions()
                                .SetSlidingExpiration(TimeSpan.FromSeconds(3600)));
                        }
                        await context.HttpContext.SignInAsync(DefaultAuthenticationScheme, cacheEntry);
                        context.HttpContext.User = cacheEntry;
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
            else
            {
                if (result?.Principal != null)
                {
                    context.HttpContext.User = result.Principal;
                }
            }
            return;
        }
    }
    public class Ids4Keys
    {
        public JsonWebKey[] Keys { get; set; }
    }
}
