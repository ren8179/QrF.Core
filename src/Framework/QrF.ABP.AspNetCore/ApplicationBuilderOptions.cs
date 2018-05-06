using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.AspNetCore
{
    public class ApplicationBuilderOptions
    {
        /// <summary>
        /// Default: true.
        /// </summary>
        public bool UseCastleLoggerFactory { get; set; }
        
        /// <summary>
        /// Default: true.
        /// </summary>
        public bool UseSecurityHeaders { get; set; }

        public ApplicationBuilderOptions()
        {
            UseCastleLoggerFactory = true;
            UseSecurityHeaders = true;
        }
    }
}
