using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Dto
{
    public class PageInput: BasePageInput
    {
        public string Name { get; set; }
        public DateTime? Begin { get; set; }
        public DateTime? End { get; set; }
    }
}
