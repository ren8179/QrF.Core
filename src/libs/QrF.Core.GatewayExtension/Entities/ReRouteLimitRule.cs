using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QrF.Core.GatewayExtension.Entities
{
    public class ReRouteLimitRule
    {
        [Key]
        public int ReRouteLimitId { get; set; }

        public int? RuleId { get; set; }

        public int? ReRouteId { get; set; }
    }
}
