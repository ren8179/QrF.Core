# QrF.Core
[<img src="https://qrframe.visualstudio.com/_apis/public/build/definitions/4623db2c-bf99-42f9-9b5b-ad03240be07a/1/badge">](https://qrframe.visualstudio.com/_apis/public/build/definitions/4623db2c-bf99-42f9-9b5b-ad03240be07a/1/badge)

基于.net core 2.2 的微服务框架

## **QrF.Core.Ids4** 基于IdentityServer4的授权认证服务

开发阶段使用`AddDeveloperSigningCredential()`方法即可完成签名认证，但是在生产环境，我们必须使用`AddSigningCredential()`方法并且使用OpenSSL生成自己的签名证书

### 使用OpenSSL生成证书

官网下载并安装OpenSSL [OpenSSL官网](https://slproweb.com/products/Win32OpenSSL.html)

下载 Win64 OpenSSL v1.1.1b 版本

在OpenSSL的bin文件夹，以管理员身份打开CMD执行以下命令：

```
openssl req -newkey rsa:2048 -nodes -keyout ids4.key -x509 -days 365 -out ids4.cer
```
下面将生成的证书和Key封装成一个文件，以便IdentityServer可以使用它们去正确地签名tokens
```
openssl pkcs12 -export -in ids4.cer -inkey ids4.key -out ids4.pfx
```


## 参考的应用框架

[jinyancao](https://github.com/jinyancao/CtrAuthPlatform)

## 参考文章

[.NET Core微服务实战-统一身份认证](https://www.cnblogs.com/jackcao/p/9928879.html)

## 开发环境
> VS2017 / VS Code

> .net core 2.2

```
旧版本已迁移至  [1.0分支](https://github.com/ren8179/QrF.Core/tree/1.0)

```