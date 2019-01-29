using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.Repositories
{
    public class RuleViolation
    {
        public RuleViolation(string parameterName, string errorMessage)
        {
            ParameterName = parameterName;
            ErrorMessage = errorMessage;
        }
        public string ParameterName
        {
            get;
            private set;
        }
        public string ErrorMessage
        {
            get;
            private set;
        }
    }
}
