using System.ComponentModel.DataAnnotations;

namespace QrF.Core.Storage.Entities
{
    /// <summary>
    /// 授权客户端
    /// </summary>
    public class Clients
    {
        /// <summary>
        /// 客户端主键
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 客户端授权ID
        /// </summary>
        [Required]
        [StringLength(50)]
        public string ClientId { get; set; }
        /// <summary>
        /// 默认 oidc
        /// </summary>
        [StringLength(50)]
        public string ProtocolType { get; set; }
        /// <summary>
        /// 验证客户端密码
        /// </summary>
        public bool RequireClientSecret { get; set; }
        /// <summary>
        /// 客户端名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string ClientName { get; set; }
        /// <summary>
        /// 客户端描述
        /// </summary>
        [StringLength(500)]
        public string Description { get; set; }
        /// <summary>
        /// 客户端地址
        /// </summary>
        [StringLength(100)]
        public string ClientUri { get; set; }
        /// <summary>
        /// 客户端logo地址
        /// </summary>
        [StringLength(150)]
        public string LogoUri { get; set; }
        /// <summary>
        /// 验证确认
        /// </summary>
        public bool RequireConsent { get; set; }
        /// <summary>
        /// 允许记住确认
        /// </summary>
        public bool AllowRememberConsent { get; set; }
        /// <summary>
        /// idtoken包含用户claim,默认否
        /// </summary>
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
        /// <summary>
        /// 验证pkce
        /// </summary>
        public bool RequirePkce { get; set; }
        /// <summary>
        /// 允许纯文本交换证明，默认否'
        /// </summary>
        public bool AllowPlainTextPkce { get; set; }
        /// <summary>
        /// 允许浏览器传输,默认0
        /// </summary>
        public bool AllowAccessTokensViaBrowser { get; set; }
        /// <summary>
        /// 指定注销地址
        /// </summary>
        [StringLength(100)]
        public string FrontChannelLogoutUri { get; set; }
        /// <summary>
        /// 注销是否发送指定地址
        /// </summary>
        public bool FrontChannelLogoutSessionRequired { get; set; } = true;
        /// <summary>
        /// 客户端注销地址
        /// </summary>
        [StringLength(500)]
        public string BackChannelLogoutUri { get; set; }
        /// <summary>
        /// 后台接收退出会话
        /// </summary>
        public bool? BackChannelLogoutSessionRequired { get; set; } = true;
        /// <summary>
        /// 允许离线接入,默认0
        /// </summary>
        public bool AllowOfflineAccess { get; set; }
        /// <summary>
        /// idtken周期,默认5分钟
        /// </summary>
        public int IdentityTokenLifetime { get; set; } = 300;
        /// <summary>
        /// AccessToken有效期,单位秒，默认1小时
        /// </summary>
        public int AccessTokenLifetime { get; set; } = 3600;
        /// <summary>
        /// 授权码过期时间,默认5分钟
        /// </summary>
        public int AuthorizationCodeLifetime { get; set; } = 300;
        /// <summary>
        /// 确认记录周期,单位秒
        /// </summary>
        public int? ConsentLifetime { get; set; }
        /// <summary>
        /// RefreshToken有效时间,单位秒，默认30天
        /// </summary>
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;
        /// <summary>
        /// 相对刷新令牌周期,单位秒，默认15天
        /// </summary>
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;
        /// <summary>
        /// ReUse OneTime
        /// </summary>
        public int RefreshTokenUsage { get; set; } = (int)1;
        /// <summary>
        /// 刷新令牌是否更新accesstoken
        /// </summary>
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        /// <summary>
        /// 令牌刷新方式 1 Absolute Sliding
        /// </summary>
        public int RefreshTokenExpiration { get; set; } = (int)1;
        /// <summary>
        /// 授权类型, 0 JWT 1 Reference,默认0
        /// </summary>
        public int AccessTokenType { get; set; } = (int)0;
        /// <summary>
        /// 是否启用本地登录,默认1
        /// </summary>
        public bool EnableLocalLogin { get; set; } = true;
        /// <summary>
        /// JWT是否嵌入唯一ID,默认0
        /// </summary>
        public bool IncludeJwtId { get; set; }
        /// <summary>
        /// 一直发送客户端申明,默认否
        /// </summary>
        public bool AlwaysSendClientClaims { get; set; }
        /// <summary>
        /// 客户端申明前缀,默认client_
        /// </summary>
        [Required]
        [StringLength(50)]
        public string ClientClaimsPrefix { get; set; } = "client_";
        [StringLength(200)]
        public string PairWiseSubjectSalt { get; set; }
        
    }
}
