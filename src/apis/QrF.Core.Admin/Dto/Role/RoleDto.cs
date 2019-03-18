using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Dto
{
    public class RoleDto
    {
        public Guid? KeyId { get; set; }
        public string Name { get; set; }
        public Guid DeptId { get; set; }
        public string Remark { get; set; }
        public string Codes { get; set; }
    }
}
