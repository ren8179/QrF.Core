using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qrf.Core.Apis.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 经销商Key
        /// </summary>
        protected string DealerKey
        {
            get
            {
                if (Request.Headers.Keys.Contains("dealerkey"))
                    return Request.Headers.FirstOrDefault(o => o.Key == "dealerkey").Value;
                return string.Empty;
            }
        }
        /// <summary>
        /// 租户Key
        /// </summary>
        protected int? TenantKey
        {
            get
            {
                if (Request.Headers.Keys.Contains("tenantkey"))
                {
                    var key = Request.Headers.FirstOrDefault(o => o.Key == "tenantkey").Value;
                    int? tid = int.Parse(key);
                    return tid < 1 ? null : tid;
                }
                return null;
            }
        }
    }
}
