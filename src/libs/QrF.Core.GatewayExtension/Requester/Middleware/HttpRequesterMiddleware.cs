using Newtonsoft.Json.Linq;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Requester;
using QrF.Core.GatewayExtension.Errors;
using QrF.Core.Utils.Extension;
using System;
using System.Net;
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
            else if ((int)response.Data.StatusCode >= (int)HttpStatusCode.BadRequest)//如果后端未处理异常，设置异常信息，统一输出，防止暴露敏感信息
            {
                var path = context.HttpContext.Request.Path.Value;
                if (IsFile(path))
                {
                    context.DownstreamResponse = new DownstreamResponse(response.Data);
                    return;
                }
                var result = await response.Data.Content.ReadAsStringAsync();
                SetPipelineError(context, GetError(response.Data.StatusCode, result, path));
                return;
            }
            context.DownstreamResponse = new DownstreamResponse(response.Data);
        }

        private Ocelot.Errors.Error GetError(HttpStatusCode code, string response, string requestPath)
        {
            try
            {
                var errorMsg = "";
                if (!response.IsNullOrEmpty())
                {
                    JObject jobj = JObject.Parse(response);
                    errorMsg = jobj["error"]?.ToString();
                    if (errorMsg.IsNullOrEmpty())
                        errorMsg = jobj["msg"]?.ToString();
                }
                errorMsg = $"路由地址 {requestPath} {code} {errorMsg}";
                switch (code)
                {
                    case HttpStatusCode.BadRequest://提取Ids4相关的异常(400)
                        return new IdentityServer4Error(errorMsg);
                    case HttpStatusCode.Unauthorized:
                        return new IdentityServer4UnauthorizedError(errorMsg);
                    case HttpStatusCode.NotFound:
                        return new NotFindDataError("未找到资源");
                    default:
                        return new InternalServerError($"请求服务异常");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"无法解析响应内容:{response}", ex);
                return new InternalServerError($"请求服务异常");
            }
        }

        private bool IsFile(string path, string fileFilt = ".jpg|.png|.ico|.txt|.gif|.jpeg")
        {
            if (path.IndexOf(".") < 0) return false;
            var ext = path.Substring(path.LastIndexOf("."));
            return fileFilt.IndexOf(ext.ToLower(), StringComparison.Ordinal) > -1;
        }
    }
}
