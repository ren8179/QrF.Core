using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QrF.ABP.AspNetCore.Configuration;
using QrF.ABP.AspNetCore.Mvc.Extensions;
using QrF.ABP.AspNetCore.Mvc.Results;
using QrF.ABP.Dependency;
using QrF.ABP.Domain.Entities;
using QrF.ABP.Events.Bus;
using QrF.ABP.Events.Bus.Exceptions;
using QrF.ABP.Logging;
using QrF.ABP.Reflections;
using QrF.ABP.Runtime.Validation;
using QrF.ABP.Web.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace QrF.ABP.AspNetCore.Mvc.ExceptionHandling
{
    public class ExceptionFilter : IExceptionFilter, ITransientDependency
    {
        public ILogger Logger { get; set; }

        public IEventBus EventBus { get; set; }

        private readonly IErrorInfoBuilder _errorInfoBuilder;
        private readonly IAspNetCoreConfiguration _configuration;

        public ExceptionFilter(IErrorInfoBuilder errorInfoBuilder, IAspNetCoreConfiguration configuration)
        {
            _errorInfoBuilder = errorInfoBuilder;
            _configuration = configuration;

            Logger = NullLogger.Instance;
            EventBus = NullEventBus.Instance;
        }

        public void OnException(ExceptionContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            var wrapResultAttribute =
                ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
                    context.ActionDescriptor.GetMethodInfo(),
                    _configuration.DefaultWrapResultAttribute
                );

            if (wrapResultAttribute.LogError)
            {
                LogHelper.LogException(Logger, context.Exception);
            }

            if (wrapResultAttribute.WrapOnError)
            {
                HandleAndWrapException(context);
            }
        }

        private void HandleAndWrapException(ExceptionContext context)
        {
            if (!ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
            {
                return;
            }

            context.HttpContext.Response.StatusCode = GetStatusCode(context);

            context.Result = new ObjectResult(
                new AjaxResponse(
                    _errorInfoBuilder.BuildForException(context.Exception)
                )
            );

            EventBus.Trigger(this, new HandledExceptionData(context.Exception));

            context.Exception = null; //Handled!
        }

        protected virtual int GetStatusCode(ExceptionContext context)
        {
            if (context.Exception is ValidationException)
            {
                return (int)HttpStatusCode.BadRequest;
            }

            if (context.Exception is EntityNotFoundException)
            {
                return (int)HttpStatusCode.NotFound;
            }

            return (int)HttpStatusCode.InternalServerError;
        }
    }
}
