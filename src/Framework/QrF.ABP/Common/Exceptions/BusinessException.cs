using System;

namespace QrF.ABP.Common.Exceptions
{
    public abstract class BusinessException : BaseException
    {
        public string ExceptionCode { get; }

        protected BusinessException() { }

        protected BusinessException(string exceptionCode) { ExceptionCode = exceptionCode; }

        protected BusinessException(string message, params object[] args) : this(string.Empty, message, args) { }

        protected BusinessException(string exceptionCode, string message, params object[] args) : this(null, exceptionCode, message, args) { }

        protected BusinessException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args) { }

        protected BusinessException(Exception innerException, string errorCode, string message, params object[] args)
            : base(string.Format(message, args), innerException) { ExceptionCode = errorCode; }
    }
}
