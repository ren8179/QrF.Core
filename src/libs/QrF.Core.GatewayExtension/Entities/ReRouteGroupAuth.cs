using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QrF.Core.GatewayExtension.Entities
{
    public class ReRouteGroupAuth
    {
        [Key]
        public int AuthId { get; set; }

        public int? GroupId { get; set; }

        public int? ReRouteId { get; set; }
    }
}
