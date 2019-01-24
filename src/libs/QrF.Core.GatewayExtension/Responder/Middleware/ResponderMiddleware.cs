using Microsoft.AspNetCore.Http;
using Ocelot.Errors;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Responder;
using QrF.Core.GatewayExtension.Configuration;
using QrF.Core.Utils.Extension;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace QrF.Core.GatewayExtension.Responder.Middleware
{
    public class ResponderMiddleware : OcelotMiddleware
    {
        private readonly OcelotRequestDelegate _next;
        private readonly IHttpResponder _responder;
        private readonly IErrorsToHttpStatusCodeMapper _codeMapper;

        public ResponderMiddleware(OcelotRequestDelegate next,
            IHttpResponder responder,
            IOcelotLoggerFactory loggerFactory,
            IErrorsToHttpStatusCodeMapper codeMapper
           )
            : base(loggerFactory.CreateLogger<ResponderMiddleware>())
        {
            _next = next;
            _responder = responder;
            _codeMapper = codeMapper;
        }

        public async Task Invoke(DownstreamContext context)
        {
            await _next.Invoke(context);
            if (context.IsError)
            {
                var errmsg = context.Errors[0].Message;
                int httpstatus = _codeMapper.Map(context.Errors);
                var errResult = new ResultMsg() { Success=false, Code = httpstatus, Msg = errmsg };
                var message = errResult.ToJson().ToLower();
                context.HttpContext.Response.ContentType = "application/json;charset=utf-8";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                await context.HttpContext.Response.WriteAsync(message);
                return;
            }
            else if (context.DownstreamResponse == null)//如果管道强制终止，不做任何处理,修复未将对象实例化错误
            {

            }
            else // 继续请求下游地址返回
            {
                Logger.LogDebug("no pipeline errors, setting and returning completed response");
                await _responder.SetResponseOnHttpContext(context.HttpContext, context.DownstreamResponse);
            }
        }

        private void SetErrorResponse(HttpContext context, List<Error> errors)
        {
            var statusCode = _codeMapper.Map(errors);
            _responder.SetErrorResponseOnContext(context, statusCode);
        }
    }
}
