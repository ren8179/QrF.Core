using QrF.Core.ComFr.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace QrF.Core.ComFr.Modules.User.Models
{
    [Table("Users")]
    public class UserEntity : EditorEntity, IUser, IIdentity
    {
        [Key]
        public string UserID { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        [NotMapped]
        public string PassWordNew { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }
        /// <summary>
        /// 登陆IP
        /// </summary>
        public string LoginIP { get; set; }
        public string PhotoUrl { get; set; }
        public int? UserTypeCD { get; set; }
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime? LastLoginDate { get; set; }
        [NotMapped]
        public override string Title { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }
        public string Email { get; set; }
        public string ApiLoginToken { get; set; }
        
        [NotMapped]
        public string AuthenticationType { get; set; }
        [NotMapped]
        public bool IsAuthenticated { get; set; }
        [NotMapped]
        public string Name { get { return UserID; } }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenDate { get; set; }
    }
}
