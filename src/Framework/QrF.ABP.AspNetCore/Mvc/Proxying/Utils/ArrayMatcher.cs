﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.AspNetCore.Mvc.Proxying.Utils
{
    internal static class ArrayMatcher
    {
        public static T[] Match<T>(T[] sourceArray, T[] destinationArray)
        {
            var result = new List<T>();

            var currentMethodParamIndex = 0;
            var parentItem = default(T);

            foreach (var sourceItem in sourceArray)
            {
                if (currentMethodParamIndex < destinationArray.Length)
                {
                    var destinationItem = destinationArray[currentMethodParamIndex];

                    if (EqualityComparer<T>.Default.Equals(sourceItem, destinationItem))
                    {
                        parentItem = default(T);
                        currentMethodParamIndex++;
                    }
                    else
                    {
                        if (parentItem == null)
                        {
                            parentItem = destinationItem;
                            currentMethodParamIndex++;
                        }
                    }
                }

                var resultItem = EqualityComparer<T>.Default.Equals(parentItem, default(T)) ? sourceItem : parentItem;
                result.Add(resultItem);
            }

            return result.ToArray();
        }
    }
}
