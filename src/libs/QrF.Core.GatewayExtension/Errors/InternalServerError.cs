using Ocelot.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.GatewayExtension.Errors
{
    public class InternalServerError : Error
    {
        public InternalServerError(string message) : base(message, OcelotErrorCode.UnableToCompleteRequestError)
        {

        }
    }
}
