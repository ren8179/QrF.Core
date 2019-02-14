using Ocelot.Middleware.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.GatewayExtension.Requester.Middleware
{
    public static class HttpRequesterMiddlewareExtensions
    {
        public static IOcelotPipelineBuilder UseCusHttpRequesterMiddleware(this IOcelotPipelineBuilder builder)
        {
            return builder.UseMiddleware<HttpRequesterMiddleware>();
        }
    }
}
