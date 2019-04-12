using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Domain
{
    /// <summary>
    /// 授权组表,记录授权组里可访问的权限
    /// </summary>
    [SugarTable("AuthGroup")]
    public class AuthGroup
    {
        /// <summary>
        /// 授权组主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int GroupId { get; set; }
        /// <summary>
        /// 授权组名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string GroupName { get; set; }
        /// <summary>
        /// 授权组描述
        /// </summary>
        [StringLength(500)]
        public string GroupDetail { get; set; }
        /// <summary>
        /// 当前状态, 1 有效 0 无效
        /// </summary>
        public int InfoStatus { get; set; }
    }
}
