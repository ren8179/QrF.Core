using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Dto
{
    public class QueryRolesInput : BasePageInput
    {
        public Guid? DeptId { get; set; }
    }
}
