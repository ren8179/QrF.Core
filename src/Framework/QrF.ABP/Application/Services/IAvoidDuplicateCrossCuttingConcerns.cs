using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Application.Services
{
    public interface IAvoidDuplicateCrossCuttingConcerns
    {
        List<string> AppliedCrossCuttingConcerns { get; }
    }
}
