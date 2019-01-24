using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QrF.Core.GatewayExtension.Entities
{
    public class ClientLimitGroup
    {
        [Key]
        public int ClientLimitGroupId { get; set; }

        public int? Id { get; set; }

        public int? LimitGroupId { get; set; }
    }
}
