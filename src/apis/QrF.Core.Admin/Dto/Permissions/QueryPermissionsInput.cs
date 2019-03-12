using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Dto
{
    public class QueryPermissionsInput : BasePageInput
    {
        public int? Types { get; set; }
    }
}
