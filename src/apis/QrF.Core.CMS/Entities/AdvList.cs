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
    [Table("AdvList")]
    public class AdvListEntity : EditorEntity
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 栏目ID
        /// </summary>
        public int ClassId { get; set; }
        /// <summary>
        /// 广告位类型
        /// </summary>
        public int Types { get; set; } = 1;
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkUrl { get; set; }
        /// <summary>
        /// 链接描述
        /// </summary>
        public string LinkSummary { get; set; }
        /// <summary>
        /// 打开窗口类型
        /// </summary>
        public string Target { get; set; } = "_blank";
        /// <summary>
        /// 是否启用时间显示
        /// </summary>
        public bool IsTimeLimit { get; set; } = false;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 点击量
        /// </summary>
        public int Hits { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        public int Sort { get; set; } = 0;
    }
}
