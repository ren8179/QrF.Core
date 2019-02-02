using QrF.Core.ComFr.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.CMS.Entities
{
    [Table("Article")]
    public class ArticleEntity : EditorEntity, IImage
    {
        [Key]
        public int ID { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public int? Counter { get; set; }
        public string ArticleContent { get; set; }
        public string ImageThumbUrl { get; set; }
        public string ImageUrl { get; set; }
        public int? ArticleTypeID { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool IsPublish { get; set; }
    }
}
