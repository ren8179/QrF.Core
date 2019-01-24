using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QrF.Core.GatewayExtension.Entities
{
    public class ClientGroup
    {
        [Key]
        public int ClientGroupId { get; set; }

        public int? Id { get; set; }

        public int? GroupId { get; set; }
    }
}
