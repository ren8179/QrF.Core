﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Extension
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                var statusCode = context.Response.StatusCode;
                if (ex is ArgumentException) statusCode = 200;
                HandleException(context, statusCode, ex.Message);
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                if (statusCode >= 400)
                {
                    var msg = GetMsg(statusCode);
                    if (!string.IsNullOrEmpty(msg))
                        HandleException(context, statusCode, msg);
                }
            }
        }

        private void HandleException(HttpContext context, int statusCode, string msg)
        {
            context.Response.ContentType = "application/json;charset=utf-8";
            context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Code = statusCode,
                Success = false,
                Msg = msg
            }).ToLower());
        }
        private string GetMsg(int statusCode)
        {
            switch (statusCode)
            {
                case 400: return "请求失败";
                case 401: return "未授权";
                case 404: return "未找到服务";
                case 502: return "无效响应";
                default: return "未知错误";
            }
        }
    }
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
