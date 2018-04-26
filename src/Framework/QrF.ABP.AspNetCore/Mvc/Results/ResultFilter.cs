using Microsoft.AspNetCore.Mvc.Filters;
using QrF.ABP.AspNetCore.Configuration;
using QrF.ABP.AspNetCore.Mvc.Extensions;
using QrF.ABP.AspNetCore.Mvc.Results.Wrapping;
using QrF.ABP.Dependency;
using QrF.ABP.Reflections;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.AspNetCore.Mvc.Results
{
    public class ResultFilter : IResultFilter, ITransientDependency
    {
        private readonly IAspNetCoreConfiguration _configuration;
        private readonly IActionResultWrapperFactory _actionResultWrapperFactory;

        public ResultFilter(IAspNetCoreConfiguration configuration,
            IActionResultWrapperFactory actionResultWrapper)
        {
            _configuration = configuration;
            _actionResultWrapperFactory = actionResultWrapper;
        }

        public virtual void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            var methodInfo = context.ActionDescriptor.GetMethodInfo();

            //var clientCacheAttribute = ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
            //    methodInfo,
            //    _configuration.DefaultClientCacheAttribute
            //);

            //clientCacheAttribute?.Apply(context);

            var wrapResultAttribute =
                ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
                    methodInfo,
                    _configuration.DefaultWrapResultAttribute
                );

            if (!wrapResultAttribute.WrapOnSuccess)
            {
                return;
            }

            _actionResultWrapperFactory.CreateFor(context).Wrap(context);
        }

        public virtual void OnResultExecuted(ResultExecutedContext context)
        {
            //no action
        }
    }
}
