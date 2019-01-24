using Ocelot.Middleware.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.GatewayExtension.Responder.Middleware
{
    public static class ResponderMiddlewareExtensions
    {
        public static IOcelotPipelineBuilder UseResponderMiddleware(this IOcelotPipelineBuilder builder)
        {
            return builder.UseMiddleware<ResponderMiddleware>();
        }
    }
}
