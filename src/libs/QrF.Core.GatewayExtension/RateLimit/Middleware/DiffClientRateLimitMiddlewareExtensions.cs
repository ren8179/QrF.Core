using Ocelot.Middleware.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.GatewayExtension.RateLimit.Middleware
{
    /// <summary>
    /// 
    /// </summary>
   public static class DiffClientRateLimitMiddlewareExtensions
    {
        public static IOcelotPipelineBuilder UseDiffClientRateLimitMiddleware(this IOcelotPipelineBuilder builder)
        {
            return builder.UseMiddleware<DiffClientRateLimitMiddleware>();
        }
    }
}
