using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Domain
{
    [SugarTable("ClientGrantTypes")]
    public class ClientGrantType
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string GrantType { get; set; }
        public int ClientId { get; set; }
    }
}
