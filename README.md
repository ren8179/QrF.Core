# QrF.Core
[<img src="https://qrframe.visualstudio.com/_apis/public/build/definitions/4623db2c-bf99-42f9-9b5b-ad03240be07a/1/badge">](https://qrframe.visualstudio.com/_apis/public/build/definitions/4623db2c-bf99-42f9-9b5b-ad03240be07a/1/badge)

基于.net core 2.0 的微服务框架

服务介绍
## QrF.Core.Materials material 示例项目
> 基于CQRS实现的应用
## QrF.Core.API webapi接口项目，对外提供数据接口

> 使用 Microsoft.AspNetCore.Mvc.Versioning 来实现版本控制

> 使用 Swashbuckle.AspNetCore 来实现接口文档展示

[<img src="https://github.com/ren8179/QrF.Core/blob/master/doc/20180413064433.png">](https://github.com/ren8179/QrF.Core/blob/master/doc/20180413064433.png)

[<img src="https://github.com/ren8179/QrF.Core/blob/master/doc/20180413064622.png">](https://github.com/ren8179/QrF.Core/blob/master/doc/20180413064622.png)

## QrF.Core.IdentityServer 身份认证服务，实现单点登录

> 使用 Identity Server4 实现身份认证

## QrF.Core.UI 客户端界面
纯前端项目，使用oidc-client来实现授权认证。
配置项:

```
authority是IdentityServer的入口URL. 通过这个URL，oidc-client可以查询如何与这个IdentityServer通信， 并验证token的有效性。
client_id 这是客户端标识，认证服务器用这个标识来区别不同的客户端。
popup_redirect_uri 是使用signinPopup方法是的重定向URL。如果你不想用弹出框来登陆，希望用户能到主登录界面登陆，那么你需要使用redirect_uri属性和signinRedirect 方法。
response_type 定义响应类型，在我们的例子中，我们只需要服务器返回身份令牌
scope 定义了我们要求的作用域
filterProtocolClaims 告诉oidc-client过滤掉OIDC协议内部用的声明信息，如: nonce, at_hash, iat, nbf, exp, aud, iss 和 idp

```
