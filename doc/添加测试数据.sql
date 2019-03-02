USE GatewayDb
GO

--插入全局测试信息
insert into GlobalConfiguration(GatewayName,RequestIdKey,IsDefault,InfoStatus)
values('网关','gateway',1,1);

--插入路由分类测试信息
insert into ReRoutesItem(ItemName,InfoStatus) values('分类1',1);

--插入路由测试信息 
insert into ReRoute values(1,'/api1/values','[ "GET" ]','','http','/api/Values','[{"Host": "localhost","Port": 5000 }]','','testapi1','','','','','',0,1);
insert into ReRoute values(1,'/api1/values/{id}','[ "GET" ]','','http','/api/Values/{id}','[{"Host": "localhost","Port": 5000 }]','','testapi2','{ "TtlSeconds": 60, "Region": "test_ocelot" }','','','','',0,1);
--插入网关关联表
insert into ConfigReRoutes values(1,1);
insert into ConfigReRoutes values(1,2);

--插入测试客户端
INSERT INTO Clients(ClientId,ClientName) VALUES('client1','测试客户端1')
INSERT INTO Clients(ClientId,ClientName) VALUES('client2','测试客户端2')

--插入测试授权组
INSERT INTO AuthGroup VALUES('用户组1','只能访问部分路由',1);
INSERT INTO AuthGroup VALUES('管理员组','能访问所有路由',1);

--插入测试组权限
INSERT INTO ReRouteGroupAuth VALUES(1,1);

INSERT INTO ReRouteGroupAuth VALUES(2,1);
INSERT INTO ReRouteGroupAuth VALUES(2,2);

--插入客户端授权
INSERT INTO ClientGroup VALUES(1,1);
INSERT INTO ClientGroup VALUES(2,2);

--设置测试路由只有授权才能访问
UPDATE ReRoute SET AuthenticationOptions='{"AuthenticationProviderKey": "TestKey"}' WHERE ReRouteId IN(1,2);

-- 授权

--2、添加客户端密钥,密码为(secreta) sha256
INSERT INTO ClientSecrets VALUES(null,'2tytAAysa0zaDuNthsfLdjeEtZSyWw8WzbzM8pfTGNI=',null,'SharedSecret',getdate(),1);
INSERT INTO ClientSecrets VALUES(null,'2tytAAysa0zaDuNthsfLdjeEtZSyWw8WzbzM8pfTGNI=',null,'SharedSecret',getdate(),2);

--3、增加客户端授权权限
INSERT INTO ClientGrantTypes VALUES('client_credentials', 1);
INSERT INTO ClientGrantTypes VALUES('client_credentials', 2);
--4、增加客户端能够访问scope
INSERT INTO ClientScopes VALUES('gateway', 1);
INSERT INTO ClientScopes VALUES('gateway', 2);
INSERT INTO ClientScopes VALUES('gateway_admin', 1);

INSERT INTO ApiResources VALUES(1,'gateway','gateway',NULL,getdate(),NULL,NULL,0);
INSERT INTO ApiResources VALUES(1,'gateway_admin','gateway_admin',NULL,getdate(),NULL,NULL,0);

INSERT INTO ApiScopes VALUES('gateway','gateway',NULL,0,0,1,1);
INSERT INTO ApiScopes VALUES('gateway_admin','gateway_admin',NULL,0,0,1,1);

-- 1、插入认证路由(使用默认分类)
insert into ReRoute values(1,'/connect/token','[ "POST" ]','','http','/connect/token','[{"Host": "localhost","Port": 6666 }]','','','','','','','',0,1);
--2、加入全局配置
INSERT INTO ConfigReRoutes VALUES(1,3)
--3、增加认证配置地址路由
insert into ReRoute values(1,'/.well-known/openid-configuration','[ "GET" ]','','http','/.well-known/openid-configuration','[{"Host": "localhost","Port": 6666 }]','','','','','','','',0,1);
--4、加入全局配置
INSERT INTO ConfigReRoutes VALUES(1,4);
--5、增加认证配置地址路由
insert into ReRoute values(1,'/.well-known/openid-configuration/jwks','[ "GET" ]','','http','/.well-known/openid-configuration/jwks','[{"Host": "localhost","Port": 6666 }]','','','','','','','',0,1);
--6、加入全局配置
INSERT INTO ConfigReRoutes VALUES(1,5);

-- 添加测试用户 admin/admin
INSERT [dbo].[Sys_User] ([KeyId], [DeptId], [Account], [Password], [NickName], [Mobile], [Email], [RealName], [Sex], [Status], [HeadPic], [UpLoginDate], [Remark], [CreateId], [CreateTime]) VALUES (N'bc3831d7-db35-4898-8f3d-3744ed95569b', N'd64e7820-eb33-428a-9cf9-df08b63b186e', N'admin', N'21232F297A57A5A743894A0E4A801FC3', N'管理员', NULL, NULL, N'管理员', NULL, 1, NULL, NULL, NULL, NULL, getdate())
GO