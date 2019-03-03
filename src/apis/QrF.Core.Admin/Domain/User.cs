using SqlSugar;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace QrF.Core.Admin.Domain
{
    [SugarTable("Sys_User")]
    public class User
    {
        /// <summary>
        /// 资源唯一标示符
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public Guid KeyId { get; set; }
        /// <summary>
        /// 归属部门
        /// </summary>
        public Guid DeptId { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadPic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 盐值
        /// </summary>
        public string Salt { set; get; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? UpLoginDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? CreateId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        [SugarColumn(IsIgnore = true)]
        public List<Claim> Claims
        {
            get
            {
                return new List<Claim>() {
                    new Claim("nickname",NickName??""),
                    new Claim("email",Email??""),
                    new Claim("mobile",Mobile??"")
                };
            }
        }
    }
}
