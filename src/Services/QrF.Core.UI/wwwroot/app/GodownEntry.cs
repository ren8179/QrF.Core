using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
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
    [Table("G_GodownEntry")]
    public class GodownEntry : FullAuditedEntity, IMayHaveTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 库房ID
        /// </summary>
        public int WareHouseId { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        [MaxLength(20)]
        public string InvoiceNo { get; set; }

        /// <summary>
        /// 经销商
        /// </summary>
        [MaxLength(100)]
        public string Dealer { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        [MaxLength(100)]
        public string ContractNo { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 入库单明细
        /// </summary>
        public List<GodownEntryItem> Items { get; set; }

        /// <summary>
        /// 入库单状态
        /// </summary>
        public GodownEntryState GeState { get; set; }

    }

    /// <summary>
    /// 入库单状态
    /// </summary>
    public enum GodownEntryState : byte
    {
        Saved = 0,
        Submitted = 1,
        Audited = 2
    }

}
