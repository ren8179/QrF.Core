using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Dto
{
    public class MsgResultDto
    {
        public int Code { get; set; }
        public bool Success { get; set; }
        public string Msg { get; set; }
    }
}
