using QrF.Core.ComFr.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QrF.Core.CMS.Entities
{
    [Table("Navigation")]
    public class NavigationEntity : EditorEntity
    {
        [Key]
        public string ID { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsMobile { get; set; }
        public string ParentId { get; set; }
        public string Url { get; set; }
        public string Html { get; set; }
        [NotMapped]
        public bool IsCurrent { get; set; }
    }
}
