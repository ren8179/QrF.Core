using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.CMS
{
    public interface IApplicationContextAccessor
    {
        CMSApplicationContext Current { get; }
    }
}
