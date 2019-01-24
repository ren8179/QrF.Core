using Ocelot.Middleware.Pipeline;

namespace QrF.Core.GatewayExtension.Authentication.Middleware
{
    public static class CusAuthenticationMiddlewareExtensions
    {
        public static IOcelotPipelineBuilder UseCusAuthenticationMiddleware(this IOcelotPipelineBuilder builder)
        {
            return builder.UseMiddleware<CusAuthenticationMiddleware>();
        }
    }
}
