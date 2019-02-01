using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.Mvc.Authorize
{
    public class DefaultAuthorizeAttribute : AuthorizeAttribute
    {
        public const string DefaultAuthenticationScheme = "DefaultAuthenticationScheme";
        public DefaultAuthorizeAttribute()
        {
            this.AuthenticationSchemes = DefaultAuthenticationScheme;
        }
    }
}
