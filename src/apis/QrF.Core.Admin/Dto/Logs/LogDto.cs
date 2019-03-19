using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Dto
{
    public class LogDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 所属应用
        /// </summary>
        public string Application { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessType { get; set; }
        /// <summary>
        /// 业务编号
        /// </summary>
        public int? BusinessId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>           
        public string Message { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }
}
