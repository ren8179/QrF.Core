using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QrF.Core.GatewayExtension.Entities
{
    public class ClientReRouteWhiteList
    {
        [Key]
        public int WhiteListId { get; set; }

        public int? ReRouteId { get; set; }

        public int? Id { get; set; }
    }
}
