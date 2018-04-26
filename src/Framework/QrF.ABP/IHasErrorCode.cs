using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP
{
    public interface IHasErrorCode
    {
        int Code { get; set; }
    }
}
