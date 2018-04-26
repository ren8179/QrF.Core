using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Web.Security.AntiForgery
{
    public interface IAntiForgeryManager
    {
        IAntiForgeryConfiguration Configuration { get; }

        string GenerateToken();
    }
}
