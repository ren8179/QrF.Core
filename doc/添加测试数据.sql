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

