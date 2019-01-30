using Microsoft.AspNetCore.Http;
using QrF.Core.ComFr.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QrF.Core.ComFr.Extension;

namespace QrF.Core.CMS
{
    public class CMSApplicationContext : ApplicationContext
    {

        private Uri _requestUrl;

        public CMSApplicationContext(IHttpContextAccessor httpContextAccessor) :
            base(httpContextAccessor)
        {
            Styles = new List<string>();
            Scripts = new List<string>();
        }

        public Uri RequestUrl
        {
            get
            {
                if (_requestUrl == null)
                {
                    if (HttpContextAccessor.HttpContext != null)
                    {
                        _requestUrl = new Uri(HttpContextAccessor.HttpContext.Request.Path);
                    }
                }
                return _requestUrl;
            }
            set { _requestUrl = new Uri(value.AbsoluteUri); }
        }
        public string MapPath(string path)
        {
            if (HttpContextAccessor.HttpContext != null)
            {
                return HttpContextAccessor.HttpContext.Request.MapPath(path);
            }
            return path;
        }
        /// <summary>
        /// Append system styles to page footer
        /// </summary>
        public List<string> Styles { get; set; }
        /// <summary>
        /// Append system script to page footer
        /// </summary>
        public List<string> Scripts { get; set; }
    }
}
