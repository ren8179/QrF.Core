using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.AspNetCore.Mvc.Results.Wrapping
{
    public interface IActionResultWrapper
    {
        void Wrap(ResultExecutingContext actionResult);
    }
}
