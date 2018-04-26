using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Web.Security.AntiForgery
{
    public class AntiForgeryConfiguration : IAntiForgeryConfiguration
    {
        public string TokenCookieName { get; set; }

        public string TokenHeaderName { get; set; }

        public AntiForgeryConfiguration()
        {
            TokenCookieName = "XSRF-TOKEN";
            TokenHeaderName = "X-XSRF-TOKEN";
        }
    }
}
