using QrF.ABP.Domain.Entities;
using QrF.ABP.Extensions;
using QrF.ABP.Runtime.Validation;
using QrF.ABP.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QrF.ABP.Web.Models
{
    internal class DefaultErrorInfoConverter : IExceptionToErrorInfoConverter
    {
        private readonly IWebModuleConfiguration _configuration;

        public IExceptionToErrorInfoConverter Next { set; private get; }

        private bool SendAllExceptionsToClients
        {
            get
            {
                return _configuration.SendAllExceptionsToClients;
            }
        }

        public DefaultErrorInfoConverter(
            IWebModuleConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ErrorInfo Convert(Exception exception)
        {
            var errorInfo = CreateErrorInfoWithoutCode(exception);

            if (exception is IHasErrorCode)
            {
                errorInfo.Code = (exception as IHasErrorCode).Code;
            }

            return errorInfo;
        }

        private ErrorInfo CreateErrorInfoWithoutCode(Exception exception)
        {
            if (SendAllExceptionsToClients)
            {
                return CreateDetailedErrorInfoFromException(exception);
            }

            if (exception is AggregateException && exception.InnerException != null)
            {
                var aggException = exception as AggregateException;
                if (aggException.InnerException is ValidationException)
                {
                    exception = aggException.InnerException;
                }
            }
            
            if (exception is ValidationException)
            {
                return new ErrorInfo("Validation Error")
                {
                    ValidationErrors = GetValidationErrorInfos(exception as ValidationException),
                    Details = GetValidationErrorNarrative(exception as ValidationException)
                };
            }

            if (exception is EntityNotFoundException)
            {
                var entityNotFoundException = exception as EntityNotFoundException;

                if (entityNotFoundException.EntityType != null)
                {
                    return new ErrorInfo(
                        string.Format("Entity Not Found",
                            entityNotFoundException.EntityType.Name,
                            entityNotFoundException.Id
                        )
                    );
                }

                return new ErrorInfo(
                    entityNotFoundException.Message
                );
            }
            
            return new ErrorInfo("Internal Server Error");
        }

        private ErrorInfo CreateDetailedErrorInfoFromException(Exception exception)
        {
            var detailBuilder = new StringBuilder();

            AddExceptionToDetails(exception, detailBuilder);

            var errorInfo = new ErrorInfo(exception.Message, detailBuilder.ToString());

            if (exception is ValidationException)
            {
                errorInfo.ValidationErrors = GetValidationErrorInfos(exception as ValidationException);
            }

            return errorInfo;
        }

        private void AddExceptionToDetails(Exception exception, StringBuilder detailBuilder)
        {
            //Exception Message
            detailBuilder.AppendLine(exception.GetType().Name + ": " + exception.Message);
            
            //Additional info for AbpValidationException
            if (exception is ValidationException)
            {
                var validationException = exception as ValidationException;
                if (validationException.ValidationErrors.Count > 0)
                {
                    detailBuilder.AppendLine(GetValidationErrorNarrative(validationException));
                }
            }

            //Exception StackTrace
            if (!string.IsNullOrEmpty(exception.StackTrace))
            {
                detailBuilder.AppendLine("STACK TRACE: " + exception.StackTrace);
            }

            //Inner exception
            if (exception.InnerException != null)
            {
                AddExceptionToDetails(exception.InnerException, detailBuilder);
            }

            //Inner exceptions for AggregateException
            if (exception is AggregateException)
            {
                var aggException = exception as AggregateException;
                if (aggException.InnerExceptions.IsNullOrEmpty())
                {
                    return;
                }

                foreach (var innerException in aggException.InnerExceptions)
                {
                    AddExceptionToDetails(innerException, detailBuilder);
                }
            }
        }

        private ValidationErrorInfo[] GetValidationErrorInfos(ValidationException validationException)
        {
            var validationErrorInfos = new List<ValidationErrorInfo>();

            foreach (var validationResult in validationException.ValidationErrors)
            {
                var validationError = new ValidationErrorInfo(validationResult.ErrorMessage);

                if (validationResult.MemberNames != null && validationResult.MemberNames.Any())
                {
                    validationError.Members = validationResult.MemberNames.Select(m => m.ToCamelCase()).ToArray();
                }

                validationErrorInfos.Add(validationError);
            }

            return validationErrorInfos.ToArray();
        }

        private string GetValidationErrorNarrative(ValidationException validationException)
        {
            var detailBuilder = new StringBuilder();
            detailBuilder.AppendLine("Validation Narrative Title");

            foreach (var validationResult in validationException.ValidationErrors)
            {
                detailBuilder.AppendFormat(" - {0}", validationResult.ErrorMessage);
                detailBuilder.AppendLine();
            }

            return detailBuilder.ToString();
        }
        
    }
}
