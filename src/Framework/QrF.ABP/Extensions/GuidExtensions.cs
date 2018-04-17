using System;

namespace QrF.ABP.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsEmpty(this Guid value) => value == Guid.Empty;
    }
}
