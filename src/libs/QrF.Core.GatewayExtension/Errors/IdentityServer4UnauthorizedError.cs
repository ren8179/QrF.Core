using Ocelot.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.GatewayExtension.Errors
{
    public class IdentityServer4UnauthorizedError : Error
    {
        public IdentityServer4UnauthorizedError(string message) : base(message, OcelotErrorCode.UnauthenticatedError)
        {

        }
    }
}
