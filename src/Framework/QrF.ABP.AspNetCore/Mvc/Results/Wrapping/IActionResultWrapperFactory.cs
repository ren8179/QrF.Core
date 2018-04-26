using Microsoft.AspNetCore.Mvc.Filters;
using QrF.ABP.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.AspNetCore.Mvc.Results.Wrapping
{
    public interface IActionResultWrapperFactory : ITransientDependency
    {
        IActionResultWrapper CreateFor(ResultExecutingContext actionResult);
    }
}
