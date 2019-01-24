using Ocelot.Configuration;
using Ocelot.Logging;
using Ocelot.Middleware;
using QrF.Core.GatewayExtension.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.GatewayExtension.Authentication.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class CusAuthenticationMiddleware : OcelotMiddleware
    {
        private readonly OcelotRequestDelegate _next;
        private readonly CusOcelotConfiguration _options;
        private readonly ICusAuthenticationProcessor _processor;

        public CusAuthenticationMiddleware(OcelotRequestDelegate next,
            IOcelotLoggerFactory loggerFactory,
            ICusAuthenticationProcessor processor,
            CusOcelotConfiguration options)
            : base(loggerFactory.CreateLogger<CusAuthenticationMiddleware>())
        {
            _next = next;
            _processor = processor;
            _options = options;
        }

        public async Task Invoke(DownstreamContext context)
        {
            if (!context.IsError && context.HttpContext.Request.Method.ToUpper() != "OPTIONS" && IsAuthenticatedRoute(context.DownstreamReRoute))
            {
                if (!_options.ClientAuthorization)
                {
                    Logger.LogInformation($"未启用客户端认证中间件");
                    await _next.Invoke(context);
                }
                else
                {
                    Logger.LogInformation($"{context.HttpContext.Request.Path} 是认证路由. {MiddlewareName} 开始校验授权信息");
                    #region 提取客户端ID
                    var clientId = "client_cjy";
                    var path = context.DownstreamReRoute.UpstreamPathTemplate.OriginalValue; //路由地址
                    var clientClaim = context.HttpContext.User.Claims.FirstOrDefault(p => p.Type == _options.ClientKey);
                    if (!string.IsNullOrEmpty(clientClaim?.Value))
                    {//从Claims中提取客户端id
                        clientId = clientClaim?.Value;
                    }
                    #endregion
                    if (await _processor.CheckClientAuthenticationAsync(clientId, path))
                    {
                        await _next.Invoke(context);
                    }
                    else
                    {//未授权直接返回错误
                        var error = new UnauthenticatedError($"请求认证路由 {context.HttpContext.Request.Path}客户端未授权");
                        Logger.LogWarning($"路由地址 {context.HttpContext.Request.Path} 自定义认证管道校验失败. {error}");
                        SetPipelineError(context, error);
                    }
                }
            }
            else
            {
                await _next.Invoke(context);
            }

        }
        private static bool IsAuthenticatedRoute(DownstreamReRoute reRoute)
        {
            return reRoute.IsAuthenticated;
        }
    }
}
