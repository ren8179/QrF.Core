using QrF.Core.ComFr.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.CMS.Entities
{
    /// <summary>
    /// 广告栏目管理
    /// </summary>
    [Table("AdvClass")]
    public class AdvClassEntity : EditorEntity
    {
        [Key]
        public int ID { get; set; }
        public int? ParentId { get; set; }
        /// <summary>
        /// 栏位类型
        /// </summary>
        public string Flag { get; set; }
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
    }
}
