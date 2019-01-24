using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QrF.Core.GatewayExtension.Entities
{
    public class LimitGroupRule
    {
        [Key]
        public int GroupRuleId { get; set; }

        public int? LimitGroupId { get; set; }

        public int? ReRouteLimitId { get; set; }
    }
}
