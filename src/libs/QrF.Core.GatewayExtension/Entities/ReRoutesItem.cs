using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QrF.Core.GatewayExtension.Entities
{
    /// <summary>
    /// 路由分类表
    /// </summary>
    public class ReRoutesItem
    {
        /// <summary>
        /// 分类主键
        /// </summary>
        [Key]
        public int ItemId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ItemName { get; set; }
        /// <summary>
        /// 分类描述
        /// </summary>
        [StringLength(500)]
        public string ItemDetail { get; set; }
        /// <summary>
        /// 上级分类,顶级节点为空
        /// </summary>
        public int? ItemParentId { get; set; }
        /// <summary>
        /// 当前状态, 1 有效 0 无效
        /// </summary>
        public int InfoStatus { get; set; }
    }
}
