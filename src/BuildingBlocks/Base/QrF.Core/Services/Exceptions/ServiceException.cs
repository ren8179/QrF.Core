using QrF.Core.Common.Exceptions;
using System;

namespace QrF.Core.Services.Exceptions
{
    public class ServiceException : BusinessException
    {
        public ServiceException() { }

        public ServiceException(string code) : base(code) { }

        public ServiceException(string message, params object[] args) : base(string.Empty, message, args) { }

        public ServiceException(string code, string message, params object[] args) : base(null, code, message, args) { }

        public ServiceException(Exception innerException, string message, params object[] args)
            : base(innerException, string.Empty, message, args) { }

        public ServiceException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException) { }
    }
}
