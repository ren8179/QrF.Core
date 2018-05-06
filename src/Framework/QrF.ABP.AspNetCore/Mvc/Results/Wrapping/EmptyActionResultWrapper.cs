using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QrF.ABP.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.AspNetCore.Mvc.Results.Wrapping
{
    public class EmptyActionResultWrapper : IActionResultWrapper
    {
        public void Wrap(ResultExecutingContext actionResult)
        {
            actionResult.Result = new ObjectResult(new AjaxResponse());
        }
    }
}
