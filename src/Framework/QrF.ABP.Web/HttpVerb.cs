using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Web
{
    /// <summary>
    /// Represents an HTTP verb.
    /// </summary>
    [Flags]
    public enum HttpVerb
    {
        /// <summary>
        /// GET
        /// </summary>
        Get,

        /// <summary>
        /// POST
        /// </summary>
        Post,

        /// <summary>
        /// PUT
        /// </summary>
        Put,

        /// <summary>
        /// DELETE
        /// </summary>
        Delete,

        /// <summary>
        /// OPTIONS
        /// </summary>
        Options,

        /// <summary>
        /// TRACE
        /// </summary>
        Trace,

        /// <summary>
        /// HEAD
        /// </summary>
        Head,

        /// <summary>
        /// PATCH
        /// </summary>
        Patch,
    }
}
