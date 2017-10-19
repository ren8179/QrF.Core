using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QrFAbp.GodownEntryInfo
{
    /// <summary>
    /// 入库单明细
    /// </summary>
    [Table("G_GodownEntryItem")]
    public class GodownEntryItem : Entity, IMayHaveTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 入库单Id
        /// </summary>
        public int GodownEntryId { get; set; }

        /// <summary>
        /// 入库单
        /// </summary>
        [ForeignKey(nameof(GodownEntryId))]
        public GodownEntry GodownEntry { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string DeviceName { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        /// <summary>
        /// SN号
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string SNCode { get; set; }

        /// <summary>
        /// 原值
        /// </summary>
        [Required]
        public decimal OriginalValue { get; set; }

        /// <summary>
        /// 最后保修日期
        /// </summary>
        [Required]
        public DateTime LastWarrantyDate { get; set; }
    }
}
