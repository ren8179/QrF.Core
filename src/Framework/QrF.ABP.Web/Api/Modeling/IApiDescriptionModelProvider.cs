using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Web.Api.Modeling
{
    public interface IApiDescriptionModelProvider
    {
        ApplicationApiDescriptionModel CreateModel();
    }
}
