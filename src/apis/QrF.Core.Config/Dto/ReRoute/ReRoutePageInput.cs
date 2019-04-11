using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Dto
{
    public class ReRoutePageInput: PageInput
    {
        public int? ItemId { get; set; }
        public int? ConfigId { get; set; }
    }
}
