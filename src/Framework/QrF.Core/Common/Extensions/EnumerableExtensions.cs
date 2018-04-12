using System;
using System.Collections.Generic;
using System.Linq;

namespace QrF.Core.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int itemsCount)
        {
            var enumerable = source as IList<T> ?? source.ToList();
            return enumerable.Skip(Math.Max(0, enumerable.Count - itemsCount));
        }
    }
}
