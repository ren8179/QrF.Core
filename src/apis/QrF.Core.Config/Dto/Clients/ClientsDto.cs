using QrF.Core.Config.Domain;
using System.Collections.Generic;

namespace QrF.Core.Config.Dto
{
    public class ClientsDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 客户端授权ID
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; set; }
        public List<string> AllowedGrantTypes { get; set; }
        public List<string> AllowedScopes { get; set; }
        public string ClientSecrets { get; set; }
        /// <summary>
        /// 客户端描述
        /// </summary>
        public string Description { get; set; }
        public bool IsAuth { get; set; }
    }
}
