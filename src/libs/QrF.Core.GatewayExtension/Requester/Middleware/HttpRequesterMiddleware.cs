using Newtonsoft.Json.Linq;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Requester;
using QrF.Core.GatewayExtension.Errors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QrF.Core.GatewayExtension.Requester.Middleware
{
    public class HttpRequesterMiddleware : OcelotMiddleware
    {
        private readonly OcelotRequestDelegate _next;
        private readonly IHttpRequester _requester;

        public HttpRequesterMiddleware(OcelotRequestDelegate next,
            IOcelotLoggerFactory loggerFactory,
            IHttpRequester requester)
                : base(loggerFactory.CreateLogger<HttpRequesterMiddleware>())
        {
            _next = next;
            _requester = requester;
        }

        public async Task Invoke(DownstreamContext context)
        {
            var response = await _requester.GetResponse(context);

            if (response.IsError)
            {
                Logger.LogDebug("IHttpRequester returned an error, setting pipeline error");

                SetPipelineError(context, response.Errors);
                return;
            }
            else if (response.Data.StatusCode != System.Net.HttpStatusCode.OK)
            {//如果后端未处理异常，设置异常信息，统一输出，防止暴露敏感信息
                if (response.Data.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {//提取Ids4相关的异常(400)
                    var result = await response.Data.Content.ReadAsStringAsync();
                    JObject jobj = JObject.Parse(result);
                    var errorMsg = jobj["error"]?.ToString();
                    var error = new IdentityServer4Error(errorMsg ?? "未知异常");
                    SetPipelineError(context, error);
                    return;
                }
                else
                {
                    var error = new InternalServerError($"请求服务异常");
                    Logger.LogWarning($"路由地址 {context.HttpContext.Request.Path} {error}");
                    SetPipelineError(context, error);
                    return;
                }
            }
            Logger.LogDebug("setting http response message");

            context.DownstreamResponse = new DownstreamResponse(response.Data);
        }
    }
}
