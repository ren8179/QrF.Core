use GatewayDb

if exists (select 1
            from  sysindexes
           where  id    = object_id('ConfigReRoutes')
            and   name  = 'Relationship_5_FK'
            and   indid > 0
            and   indid < 255)
   drop index ConfigReRoutes.Relationship_5_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ConfigReRoutes')
            and   name  = 'Relationship_4_FK'
            and   indid > 0
            and   indid < 255)
   drop index ConfigReRoutes.Relationship_4_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ConfigReRoutes')
            and   type = 'U')
   drop table ConfigReRoutes
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GlobalConfiguration')
            and   type = 'U')
   drop table GlobalConfiguration
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ReRoute')
            and   name  = '分类路由信息_FK'
            and   indid > 0
            and   indid < 255)
   drop index ReRoute.分类路由信息_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ReRoute')
            and   type = 'U')
   drop table ReRoute
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ReRoutesItem')
            and   type = 'U')
   drop table ReRoutesItem
go

/*==============================================================*/
/* Table: ConfigReRoutes                                    */
/*==============================================================*/
create table ConfigReRoutes (
   CtgRouteId           int                  identity,
   KeyId               int                  null,
   ReRouteId            int                  null,
   constraint PK_ConfigReRoutes primary key nonclustered (CtgRouteId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '网关-路由,可以配置多个网关和多个路由',
   'user', @CurrentUser, 'table', 'ConfigReRoutes'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '配置路由主键',
   'user', @CurrentUser, 'table', 'ConfigReRoutes', 'column', 'CtgRouteId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '网关主键',
   'user', @CurrentUser, 'table', 'ConfigReRoutes', 'column', 'KeyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路由主键',
   'user', @CurrentUser, 'table', 'ConfigReRoutes', 'column', 'ReRouteId'
go

/*==============================================================*/
/* Index: Relationship_4_FK                                     */
/*==============================================================*/
create index Relationship_4_FK on ConfigReRoutes (
KeyId ASC
)
go

/*==============================================================*/
/* Index: Relationship_5_FK                                     */
/*==============================================================*/
create index Relationship_5_FK on ConfigReRoutes (
ReRouteId ASC
)
go

/*==============================================================*/
/* Table: GlobalConfiguration                               */
/*==============================================================*/
create table GlobalConfiguration (
   KeyId               int                  identity,
   GatewayName          varchar(100)         not null,
   RequestIdKey         varchar(100)         null,
   BaseUrl              varchar(100)         null,
   DownstreamScheme     varchar(50)          null,
   ServiceDiscoveryProvider varchar(500)         null,
   LoadBalancerOptions  varchar(500)         null,
   HttpHandlerOptions   varchar(500)         null,
   QoSOptions           varchar(200)         null,
   IsDefault            int                  not null default 0,
   InfoStatus           int                  not null default 1,
   constraint PK_GlobalConfiguration primary key nonclustered (KeyId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '网关全局配置表',
   'user', @CurrentUser, 'table', 'GlobalConfiguration'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '网关主键',
   'user', @CurrentUser, 'table', 'GlobalConfiguration', 'column', 'KeyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '网关名称',
   'user', @CurrentUser, 'table', 'GlobalConfiguration', 'column', 'GatewayName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '全局请求默认key',
   'user', @CurrentUser, 'table', 'GlobalConfiguration', 'column', 'RequestIdKey'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '请求路由根地址',
   'user', @CurrentUser, 'table', 'GlobalConfiguration', 'column', 'BaseUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '下游使用架构',
   'user', @CurrentUser, 'table', 'GlobalConfiguration', 'column', 'DownstreamScheme'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '服务发现全局配置,值为配置json',
   'user', @CurrentUser, 'table', 'GlobalConfiguration', 'column', 'ServiceDiscoveryProvider'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '全局负载均衡配置',
   'user', @CurrentUser, 'table', 'GlobalConfiguration', 'column', 'LoadBalancerOptions'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Http请求配置',
   'user', @CurrentUser, 'table', 'GlobalConfiguration', 'column', 'HttpHandlerOptions'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '请求安全配置,超时、重试、熔断',
   'user', @CurrentUser, 'table', 'GlobalConfiguration', 'column', 'QoSOptions'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否默认配置, 1 默认 0 默认',
   'user', @CurrentUser, 'table', 'GlobalConfiguration', 'column', 'IsDefault'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '当前状态, 1 有效 0 无效',
   'user', @CurrentUser, 'table', 'GlobalConfiguration', 'column', 'InfoStatus'
go

/*==============================================================*/
/* Table: ReRoute                                           */
/*==============================================================*/
create table ReRoute (
   ReRouteId            int                  identity,
   ItemId               int                  null,
   UpstreamPathTemplate varchar(150)         not null,
   UpstreamHttpMethod   varchar(50)          not null,
   UpstreamHost         varchar(100)         null,
   DownstreamScheme     varchar(50)          not null,
   DownstreamPathTemplate varchar(200)         not null,
   DownstreamHostAndPorts varchar(500)         null,
   AuthenticationOptions varchar(300)         null,
   RequestIdKey         varchar(100)         null,
   CacheOptions         varchar(200)         null,
   ServiceName          varchar(100)         null,
   LoadBalancerOptions  varchar(500)         null,
   QoSOptions           varchar(200)         null,
   DelegatingHandlers   varchar(200)         null,
   Priority             int                  null,
   InfoStatus           int                  not null default 1,
   constraint PK_ReRoute primary key nonclustered (ReRouteId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路由配置表',
   'user', @CurrentUser, 'table', 'ReRoute'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路由主键',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'ReRouteId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分类主键',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'ItemId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '上游路径模板，支持正则',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'UpstreamPathTemplate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '上游请求方法数组格式',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'UpstreamHttpMethod'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '上游域名地址',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'UpstreamHost'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '下游使用架构',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'DownstreamScheme'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '下游路径模板,与上游正则对应',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'DownstreamPathTemplate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '下游请求地址和端口,静态负载配置',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'DownstreamHostAndPorts'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '授权配置,是否需要认证访问',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'AuthenticationOptions'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '全局请求默认key',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'RequestIdKey'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '缓存配置,常用查询和再次配置缓存',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'CacheOptions'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '服务发现名称,启用服务发现时生效',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'ServiceName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '全局负载均衡配置',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'LoadBalancerOptions'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '请求安全配置,超时、重试、熔断',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'QoSOptions'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '委托处理方法,特定路由定义委托单独处理',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'DelegatingHandlers'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路由优先级,多个路由匹配上，优先级高的先执行',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'Priority'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '当前状态, 1 有效 0 无效',
   'user', @CurrentUser, 'table', 'ReRoute', 'column', 'InfoStatus'
go

/*==============================================================*/
/* Index: 分类路由信息_FK                                             */
/*==============================================================*/
create index 分类路由信息_FK on ReRoute (
ItemId ASC
)
go

/*==============================================================*/
/* Table: ReRoutesItem                                      */
/*==============================================================*/
create table ReRoutesItem (
   ItemId               int                  identity,
   ItemName             varchar(100)         not null,
   ItemDetail           varchar(500)         null,
   ItemParentId         int                  null,
   InfoStatus           int                  not null default 1,
   constraint PK_ReRouteSITEM primary key nonclustered (ItemId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路由分类表',
   'user', @CurrentUser, 'table', 'ReRoutesItem'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分类主键',
   'user', @CurrentUser, 'table', 'ReRoutesItem', 'column', 'ItemId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分类名称',
   'user', @CurrentUser, 'table', 'ReRoutesItem', 'column', 'ItemName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分类描述',
   'user', @CurrentUser, 'table', 'ReRoutesItem', 'column', 'ItemDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '上级分类,顶级节点为空',
   'user', @CurrentUser, 'table', 'ReRoutesItem', 'column', 'ItemParentId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '当前状态, 1 有效 0 无效',
   'user', @CurrentUser, 'table', 'ReRoutesItem', 'column', 'InfoStatus'
go

/*==============================================================*/
/* Table: AuthGroup                                     */
/*==============================================================*/
CREATE TABLE [dbo].[AuthGroup](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [varchar](100) NOT NULL,
	[GroupDetail] [varchar](500) NULL,
	[InfoStatus] [int] NOT NULL,
 CONSTRAINT [PK_AuthGroup] PRIMARY KEY NONCLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AuthGroup] ADD  DEFAULT ((1)) FOR [InfoStatus]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权组主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'GroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权组名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'GroupName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权组描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'GroupDetail'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前状态, 1 有效 0 无效' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'InfoStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权组表,记录授权组里可访问的权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup'
GO

/*==============================================================*/
/* Table: ClientGroup                                      */
/*==============================================================*/
CREATE TABLE [dbo].[ClientGroup](
	[ClientGroupId] [int] IDENTITY(1,1) NOT NULL,
	[Id] [int] NULL,
	[GroupId] [int] NULL,
 CONSTRAINT [PK_ClientGroup] PRIMARY KEY NONCLUSTERED 
(
	[ClientGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端授权主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientGroup', @level2type=N'COLUMN',@level2name=N'ClientGroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientGroup', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权组主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientGroup', @level2type=N'COLUMN',@level2name=N'GroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端配置授权组表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientGroup'
GO

/*==============================================================*/
/* Table: ClientLimitGroup                                      */
/*==============================================================*/
CREATE TABLE [dbo].[ClientLimitGroup](
	[ClientLimitGroupId] [int] IDENTITY(1,1) NOT NULL,
	[Id] [int] NULL,
	[LimitGroupId] [int] NULL,
 CONSTRAINT [PK_ClientLimitGroup] PRIMARY KEY NONCLUSTERED 
(
	[ClientLimitGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端限流组主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientLimitGroup', @level2type=N'COLUMN',@level2name=N'ClientLimitGroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientLimitGroup', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限流组主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientLimitGroup', @level2type=N'COLUMN',@level2name=N'LimitGroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端授权限流组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientLimitGroup'
GO

/*==============================================================*/
/* Table: ClientReRouteWhiteList                                      */
/*==============================================================*/
CREATE TABLE [dbo].[ClientReRouteWhiteList](
	[WhiteListId] [int] IDENTITY(1,1) NOT NULL,
	[ReRouteId] [int] NULL,
	[Id] [int] NULL,
 CONSTRAINT [PK_ClientReRouteWhiteList] PRIMARY KEY NONCLUSTERED 
(
	[WhiteListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientReRouteWhiteList', @level2type=N'COLUMN',@level2name=N'WhiteListId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路由主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientReRouteWhiteList', @level2type=N'COLUMN',@level2name=N'ReRouteId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientReRouteWhiteList', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端路由白名单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientReRouteWhiteList'
GO

/*==============================================================*/
/* Table: Clients                                      */
/*==============================================================*/
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AbsoluteRefreshTokenLifetime] [int] NOT NULL,
	[AccessTokenLifetime] [int] NOT NULL,
	[AccessTokenType] [int] NOT NULL,
	[AllowAccessTokensViaBrowser] [bit] NOT NULL,
	[AllowOfflineAccess] [bit] NOT NULL,
	[AllowPlainTextPkce] [bit] NOT NULL,
	[AllowRememberConsent] [bit] NOT NULL,
	[AlwaysIncludeUserClaimsInIdToken] [bit] NOT NULL,
	[AlwaysSendClientClaims] [bit] NOT NULL,
	[AuthorizationCodeLifetime] [int] NOT NULL,
	[BackChannelLogoutSessionRequired] [bit] NULL,
	[BackChannelLogoutUri] [varchar](500) NULL,
	[ClientClaimsPrefix] [varchar](50) NOT NULL,
	[ClientId] [varchar](50) NOT NULL,
	[ClientName] [varchar](200) NOT NULL,
	[ClientUri] [varchar](100) NULL,
	[ConsentLifetime] [int] NULL,
	[Description] [varchar](500) NULL,
	[EnableLocalLogin] [bit] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[FrontChannelLogoutSessionRequired] [bit] NOT NULL,
	[FrontChannelLogoutUri] [varchar](100) NULL,
	[IdentityTokenLifetime] [int] NOT NULL,
	[IncludeJwtId] [bit] NOT NULL,
	[LogoUri] [varchar](150) NULL,
	[PairWiseSubjectSalt] [varchar](200) NULL,
	[ProtocolType] [varchar](50) NULL,
	[RefreshTokenExpiration] [int] NOT NULL,
	[RefreshTokenUsage] [int] NOT NULL,
	[RequireClientSecret] [bit] NOT NULL,
	[RequireConsent] [bit] NOT NULL,
	[RequirePkce] [bit] NOT NULL,
	[SlidingRefreshTokenLifetime] [int] NOT NULL,
	[UpdateAccessTokenClaimsOnRefresh] [bit] NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((2592000)) FOR [AbsoluteRefreshTokenLifetime]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((3600)) FOR [AccessTokenLifetime]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((0)) FOR [AccessTokenType]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((0)) FOR [AllowAccessTokensViaBrowser]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((1)) FOR [AllowOfflineAccess]
GO

ALTER TABLE [dbo].[Clients] ADD  CONSTRAINT [DF_Clients_AllowPlainTextPkce]  DEFAULT ((0)) FOR [AllowPlainTextPkce]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((1)) FOR [AllowRememberConsent]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((0)) FOR [AlwaysIncludeUserClaimsInIdToken]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((0)) FOR [AlwaysSendClientClaims]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((300)) FOR [AuthorizationCodeLifetime]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((1)) FOR [BackChannelLogoutSessionRequired]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ('client_') FOR [ClientClaimsPrefix]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((1)) FOR [EnableLocalLogin]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((1)) FOR [Enabled]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((1)) FOR [FrontChannelLogoutSessionRequired]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((300)) FOR [IdentityTokenLifetime]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((0)) FOR [IncludeJwtId]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ('oidc') FOR [ProtocolType]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((1)) FOR [RefreshTokenExpiration]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((1)) FOR [RefreshTokenUsage]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((1)) FOR [RequireClientSecret]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((1)) FOR [RequireConsent]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((0)) FOR [RequirePkce]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((1296000)) FOR [SlidingRefreshTokenLifetime]
GO

ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((0)) FOR [UpdateAccessTokenClaimsOnRefresh]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'RefreshToken有效时间,单位秒，默认30天' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'AbsoluteRefreshTokenLifetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AccessToken有效期,单位秒，默认1小时' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'AccessTokenLifetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权类型, 0 JWT 1 Reference,默认0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'AccessTokenType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'允许浏览器传输,默认0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'AllowAccessTokensViaBrowser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'允许离线接入,默认0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'AllowOfflineAccess'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'允许纯文本交换证明，默认否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'AllowPlainTextPkce'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'允许记住确认' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'AllowRememberConsent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'idtoken包含用户claim,默认否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'AlwaysIncludeUserClaimsInIdToken'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一直发送客户端申明,默认否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'AlwaysSendClientClaims'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权码过期时间,默认5分钟' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'AuthorizationCodeLifetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台接收退出会话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'BackChannelLogoutSessionRequired'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端注销地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'BackChannelLogoutUri'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端申明前缀,默认client_' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'ClientClaimsPrefix'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端授权ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'ClientId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'ClientName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'ClientUri'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认记录周期,单位秒' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'ConsentLifetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用本地登录,默认1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'EnableLocalLogin'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否有效' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'Enabled'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注销是否发送指定地址,默认1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'FrontChannelLogoutSessionRequired'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指定注销地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'FrontChannelLogoutUri'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'idtken周期,默认5分钟' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'IdentityTokenLifetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'JWT是否嵌入唯一ID,默认0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'IncludeJwtId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端logo地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'LogoUri'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认 oidc' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'ProtocolType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'令牌刷新方式 1 Absolute Sliding ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'RefreshTokenExpiration'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' ReUse OneTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'RefreshTokenUsage'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验证客户端密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'RequireClientSecret'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验证确认' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'RequireConsent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验证pkce' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'RequirePkce'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'相对刷新令牌周期,单位秒，默认15天' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'SlidingRefreshTokenLifetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'刷新令牌是否更新accesstoken' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'UpdateAccessTokenClaimsOnRefresh'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权客户端' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients'
GO

/*==============================================================*/
/* Table: LimitGroup                                      */
/*==============================================================*/
CREATE TABLE [dbo].[LimitGroup](
	[LimitGroupId] [int] IDENTITY(1,1) NOT NULL,
	[LimitGroupName] [varchar](100) NOT NULL,
	[LimitGroupDetail] [varchar](500) NULL,
	[InfoStatus] [int] NOT NULL,
 CONSTRAINT [PK_LimitGroup] PRIMARY KEY NONCLUSTERED 
(
	[LimitGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[LimitGroup] ADD  CONSTRAINT [DF__AhphLimit__InfoS__31EC6D26]  DEFAULT ((1)) FOR [InfoStatus]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限流组主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LimitGroup', @level2type=N'COLUMN',@level2name=N'LimitGroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限流组名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LimitGroup', @level2type=N'COLUMN',@level2name=N'LimitGroupName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限流组描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LimitGroup', @level2type=N'COLUMN',@level2name=N'LimitGroupDetail'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前状态, 1 有效 0 无效' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LimitGroup', @level2type=N'COLUMN',@level2name=N'InfoStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限流组表,定义一系列限流规则，统一管理' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LimitGroup'
GO

/*==============================================================*/
/* Table: LimitGroupRule                                      */
/*==============================================================*/
CREATE TABLE [dbo].[LimitGroupRule](
	[GroupRuleId] [int] IDENTITY(1,1) NOT NULL,
	[LimitGroupId] [int] NULL,
	[ReRouteLimitId] [int] NULL,
 CONSTRAINT [PK_LimitGroupRule] PRIMARY KEY NONCLUSTERED 
(
	[GroupRuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'策略主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LimitGroupRule', @level2type=N'COLUMN',@level2name=N'GroupRuleId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限流组主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LimitGroupRule', @level2type=N'COLUMN',@level2name=N'LimitGroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路由限流规则主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LimitGroupRule', @level2type=N'COLUMN',@level2name=N'ReRouteLimitId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限流组策略表,记录分组的所有限流情况' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LimitGroupRule'
GO

/*==============================================================*/
/* Table: LimitRule                                      */
/*==============================================================*/
CREATE TABLE [dbo].[LimitRule](
	[RuleId] [int] IDENTITY(1,1) NOT NULL,
	[LimitName] [varchar](200) NOT NULL,
	[LimitPeriod] [varchar](50) NOT NULL,
	[LimitNum] [int] NOT NULL,
	[InfoStatus] [int] NOT NULL,
 CONSTRAINT [PK_LimitRule] PRIMARY KEY NONCLUSTERED 
(
	[RuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[LimitRule] ADD  CONSTRAINT [DF__AhphLimit__InfoS__2D27B809]  DEFAULT ((1)) FOR [InfoStatus]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'规则主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LimitRule', @level2type=N'COLUMN',@level2name=N'RuleId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限流规则名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LimitRule', @level2type=N'COLUMN',@level2name=N'LimitName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限流策略,支持 秒(s)  分钟(m) 小时( h) 天( d) , 比如10s' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LimitRule', @level2type=N'COLUMN',@level2name=N'LimitPeriod'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限制访问次数,大于0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LimitRule', @level2type=N'COLUMN',@level2name=N'LimitNum'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前状态, 1 有效 0 无效' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LimitRule', @level2type=N'COLUMN',@level2name=N'InfoStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限流规则表,记录限流的所有规则，可以对规则重复利用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LimitRule'
GO

/*==============================================================*/
/* Table: ReRouteGroupAuth                                      */
/*==============================================================*/
CREATE TABLE [dbo].[ReRouteGroupAuth](
	[AuthId] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NULL,
	[ReRouteId] [int] NULL,
 CONSTRAINT [PK_ReRouteGroupAuth] PRIMARY KEY NONCLUSTERED 
(
	[AuthId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReRouteGroupAuth', @level2type=N'COLUMN',@level2name=N'AuthId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权组主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReRouteGroupAuth', @level2type=N'COLUMN',@level2name=N'GroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路由主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReRouteGroupAuth', @level2type=N'COLUMN',@level2name=N'ReRouteId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权组权限表,记录授权组能够访问的路由' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReRouteGroupAuth'
GO

/*==============================================================*/
/* Table: ReRouteLimitRule                                      */
/*==============================================================*/
CREATE TABLE [dbo].[ReRouteLimitRule](
	[ReRouteLimitId] [int] IDENTITY(1,1) NOT NULL,
	[RuleId] [int] NULL,
	[ReRouteId] [int] NULL,
 CONSTRAINT [PK_ReRouteLimitRule] PRIMARY KEY NONCLUSTERED 
(
	[ReRouteLimitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路由限流规则主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReRouteLimitRule', @level2type=N'COLUMN',@level2name=N'ReRouteLimitId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'规则主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReRouteLimitRule', @level2type=N'COLUMN',@level2name=N'RuleId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路由主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReRouteLimitRule', @level2type=N'COLUMN',@level2name=N'ReRouteId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路由限流规则表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReRouteLimitRule'
GO

