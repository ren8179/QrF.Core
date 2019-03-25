using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.TestApi1.Configuration
{
    public class AppSettings
    {
        public AuthDto Auth { get; set; }
    }
    public class AuthDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string ApiName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ServerUrl { get; set; }
    }
}
