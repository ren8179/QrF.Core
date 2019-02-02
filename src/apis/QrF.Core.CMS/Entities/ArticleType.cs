using QrF.Core.ComFr.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QrF.Core.CMS.Entities
{
    [Table("ArticleType")]
    public class ArticleType : EditorEntity
    {
        [Key]
        public int ID { get; set; }
        public string Url { get; set; }
        public int? ParentID { get; set; }
        public string SEOTitle { get; set; }
        public string SEOKeyWord { get; set; }
        public string SEODescription { get; set; }
    }
}
