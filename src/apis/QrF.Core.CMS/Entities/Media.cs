using QrF.Core.ComFr.Constant;
using QrF.Core.ComFr.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.CMS.Entities
{
    [Table("CMS_Media")]
    public class MediaEntity : EditorEntity
    {
        [Key]
        public string ID { get; set; }
        public string ParentID { get; set; }
        public int MediaType { get; set; }
        public string Url { get; set; }

        public string MediaTypeImage
        {
            get { return ((MediaType)MediaType).ToString().ToLower(); }
        }
    }
}
