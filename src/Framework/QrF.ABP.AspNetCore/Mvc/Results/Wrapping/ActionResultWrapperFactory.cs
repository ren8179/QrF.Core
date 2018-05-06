using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.AspNetCore.Mvc.Results.Wrapping
{
    public class ActionResultWrapperFactory : IActionResultWrapperFactory
    {
        public IActionResultWrapper CreateFor(ResultExecutingContext actionResult)
        {
            if (actionResult.Result is ObjectResult)
            {
                return new ObjectActionResultWrapper(actionResult.HttpContext.RequestServices);
            }

            if (actionResult.Result is JsonResult)
            {
                return new JsonActionResultWrapper();
            }

            if (actionResult.Result is EmptyResult)
            {
                return new EmptyActionResultWrapper();
            }

            return new NullAbpActionResultWrapper();
        }
    }
}
