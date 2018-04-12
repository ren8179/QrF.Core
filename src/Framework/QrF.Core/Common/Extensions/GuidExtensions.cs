using System;

namespace QrF.Core.Common.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsEmpty(this Guid value) => value == Guid.Empty;
    }
}
