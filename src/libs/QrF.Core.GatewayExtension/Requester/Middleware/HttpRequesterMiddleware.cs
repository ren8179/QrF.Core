using Newtonsoft.Json.Linq;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Requester;
using Ocelot.Responses;
using QrF.Core.GatewayExtension.Errors;
using QrF.Core.Utils.Extension;
using System;
using System.Net;
using System.Net.Http;
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
            else if (response.Data.StatusCode != HttpStatusCode.OK)//如果后端未处理异常，设置异常信息，统一输出，防止暴露敏感信息
            {
                var result = await response.Data.Content.ReadAsStringAsync();
                Logger.LogDebug($"response content：{result}");
                SetPipelineError(context, GetError(response.Data.StatusCode, result, context.HttpContext.Request.Path.Value));
                return;
            }
            Logger.LogDebug("setting http response message");
            context.DownstreamResponse = new DownstreamResponse(response.Data);
        }

        private Ocelot.Errors.Error GetError(HttpStatusCode code, string response, string requestPath)
        {
            try
            {
                JObject jobj = JObject.Parse(response);
                var errorMsg = jobj["error"]?.ToString();
                if (errorMsg.IsNullOrEmpty())
                    errorMsg = jobj["msg"]?.ToString();
                Logger.LogWarning($"路由地址 {requestPath} {errorMsg}");
                switch (code)
                {
                    case HttpStatusCode.BadRequest://提取Ids4相关的异常(400)
                        return new IdentityServer4Error(errorMsg ?? "未知异常");
                    case HttpStatusCode.Unauthorized:
                        return new IdentityServer4UnauthorizedError(errorMsg ?? "未授权");
                    default:
                        return new InternalServerError($"请求服务异常");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("解析响应内容失败", ex);
                return new InternalServerError($"请求服务异常");
            }
        }
    }
}
