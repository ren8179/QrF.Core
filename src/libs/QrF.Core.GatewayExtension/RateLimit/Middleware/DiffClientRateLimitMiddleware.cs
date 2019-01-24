using Ocelot.Logging;
using Ocelot.Middleware;
using QrF.Core.GatewayExtension.Configuration;
using QrF.Core.GatewayExtension.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrF.Core.GatewayExtension.RateLimit.Middleware
{
    /// <summary>
    /// 自定义客户端限流中间件
    /// </summary>
    public class DiffClientRateLimitMiddleware : OcelotMiddleware
    {
        private readonly IClientRateLimitProcessor _clientRateLimitProcessor;
        private readonly OcelotRequestDelegate _next;
        private readonly CusOcelotConfiguration _options;
        public DiffClientRateLimitMiddleware(OcelotRequestDelegate next,
            IOcelotLoggerFactory loggerFactory,
            IClientRateLimitProcessor clientRateLimitProcessor,
            CusOcelotConfiguration options)
            : base(loggerFactory.CreateLogger<DiffClientRateLimitMiddleware>())
        {
            _next = next;
            _clientRateLimitProcessor = clientRateLimitProcessor;
            _options = options;
        }

        public async Task Invoke(DownstreamContext context)
        {
            var clientId = "client_cjy"; //使用默认的客户端
            if (!context.IsError)
            {
                if (!_options.ClientRateLimit)
                {
                    Logger.LogInformation($"未启用客户端限流中间件");
                    await _next.Invoke(context);
                }
                else
                {
                    //非认证的渠道
                    if (!context.DownstreamReRoute.IsAuthenticated)
                    {
                        if (context.HttpContext.Request.Headers.Keys.Contains(_options.ClientKey))
                        {
                            clientId = context.HttpContext.Request.Headers[_options.ClientKey].First();
                        }
                    }
                    else
                    {//认证过的渠道，从Claim中提取
                        var clientClaim = context.HttpContext.User.Claims.FirstOrDefault(p => p.Type == _options.ClientKey);
                        if (!string.IsNullOrEmpty(clientClaim?.Value))
                        {
                            clientId = clientClaim?.Value;
                        }
                    }
                    var path = context.DownstreamReRoute.UpstreamPathTemplate.OriginalValue;
                    if (await _clientRateLimitProcessor.CheckClientRateLimitResultAsync(clientId, path))
                    {
                        await _next.Invoke(context);
                    }
                    else
                    {
                        var error = new RateLimitOptionsError($"请求路由 {context.HttpContext.Request.Path}触发限流策略");
                        Logger.LogWarning($"路由地址 {context.HttpContext.Request.Path} 触发限流策略. {error}");
                        SetPipelineError(context, error);
                    }
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
