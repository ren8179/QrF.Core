﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using QrF.ABP.Web.Models;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QrF.ABP.AspNetCore.Mvc.Results.Wrapping
{
    public class ObjectActionResultWrapper : IActionResultWrapper
    {
        private readonly IServiceProvider _serviceProvider;

        public ObjectActionResultWrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Wrap(ResultExecutingContext actionResult)
        {
            var objectResult = actionResult.Result as ObjectResult;
            if (objectResult == null)
            {
                throw new ArgumentException($"{nameof(actionResult)} should be ObjectResult!");
            }

            if (!(objectResult.Value is AjaxResponseBase))
            {
                objectResult.Value = new AjaxResponse(objectResult.Value);
                if (!objectResult.Formatters.Any(f => f is JsonOutputFormatter))
                {
                    objectResult.Formatters.Add(
                        new JsonOutputFormatter(
                            _serviceProvider.GetRequiredService<IOptions<MvcJsonOptions>>().Value.SerializerSettings,
                            _serviceProvider.GetRequiredService<ArrayPool<char>>()
                        )
                    );
                }
            }
        }
    }
}
