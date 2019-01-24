using System.ComponentModel.DataAnnotations;

namespace QrF.Core.GatewayExtension.Entities
{
    public class AuthGroup
    {
        [Key]
        public int GroupId { get; set; }

        [Required]
        [StringLength(100)]
        public string GroupName { get; set; }

        [StringLength(500)]
        public string GroupDetail { get; set; }

        public int InfoStatus { get; set; }
    }
}
