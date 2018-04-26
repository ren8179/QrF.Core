using Microsoft.AspNetCore.Mvc.ModelBinding;
using QrF.ABP.Timing;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.AspNetCore.Mvc.ModelBinding
{
    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType != typeof(DateTime) &&
                context.Metadata.ModelType != typeof(DateTime?))
            {
                return null;
            }

            var dateNormalizationDisabledForClass = context.Metadata.ContainerType.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true);
            var dateNormalizationDisabledForProperty = context.Metadata.ContainerType
                                                                        .GetProperty(context.Metadata.PropertyName)
                                                                        .IsDefined(typeof(DisableDateTimeNormalizationAttribute), true);

            if (!dateNormalizationDisabledForClass && !dateNormalizationDisabledForProperty)
            {
                return new DateTimeModelBinder(context.Metadata.ModelType);
            }

            return null;
        }
    }
}
