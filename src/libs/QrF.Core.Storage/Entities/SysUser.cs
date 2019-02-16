using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace QrF.Core.Storage.Entities
{
    public class SysUser
    {
        /// <summary>
        /// 
        /// </summary>
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
        /// 上次登录时间
        /// </summary>
        public DateTime? UpLoginDate { get; set; }
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
