using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QrF.Core.GatewayExtension.Entities
{
    public class ConfigReRoutes
    {
        [Key]
        public int CtgRouteId { get; set; }

        public int? KeyId { get; set; }

        public int? ReRouteId { get; set; }
    }
}
