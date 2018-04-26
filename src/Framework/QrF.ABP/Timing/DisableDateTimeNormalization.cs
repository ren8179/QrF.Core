using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Timing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class DisableDateTimeNormalizationAttribute : Attribute
    {

    }
}
