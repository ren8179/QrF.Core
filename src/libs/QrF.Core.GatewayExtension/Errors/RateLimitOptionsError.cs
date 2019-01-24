using Ocelot.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.GatewayExtension.Errors
{
    /// <summary>
    /// 限流错误信息
    /// </summary>
    public class RateLimitOptionsError : Error
    {
        public RateLimitOptionsError(string message) : base(message, OcelotErrorCode.RateLimitOptionsError)
        {

        }
    }
}
