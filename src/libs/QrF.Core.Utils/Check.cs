﻿using JetBrains.Annotations;
using QrF.Core.Utils.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace QrF.Core.Utils
{
    [DebuggerStepThrough]
    public static class Check
    {
        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>(T value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>(T value, [InvokerParameterName] [NotNull] string parameterName, string message)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, message);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static string NotNullOrWhiteSpace(string value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentException($"{parameterName} can not be null, empty or white space!", parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static string NotNullOrEmpty(string value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException($"{parameterName} can not be null or empty!", parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static ICollection<T> NotNullOrEmpty<T>(ICollection<T> value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);
            }

            return value;
        }
    }
}
