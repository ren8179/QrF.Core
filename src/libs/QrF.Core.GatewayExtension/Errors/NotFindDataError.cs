using Ocelot.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.GatewayExtension.Errors
{
    public class NotFindDataError : Error
    {
        public NotFindDataError(string message) : base(message, OcelotErrorCode.CannotFindDataError)
        {

        }
    }
}
