using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Events.Bus.Entities
{
    public enum EntityChangeType : byte
    {
        Created = 0,
        Updated = 1,
        Deleted = 2
    }
}
