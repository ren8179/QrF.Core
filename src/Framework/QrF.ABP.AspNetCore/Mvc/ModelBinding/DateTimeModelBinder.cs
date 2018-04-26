using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using QrF.ABP.Timing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QrF.ABP.AspNetCore.Mvc.ModelBinding
{
    public class DateTimeModelBinder : IModelBinder
    {
        private readonly Type _type;
        private readonly SimpleTypeModelBinder _simpleTypeModelBinder;

        public DateTimeModelBinder(Type type)
        {
            _type = type;
            _simpleTypeModelBinder = new SimpleTypeModelBinder(type);
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            await _simpleTypeModelBinder.BindModelAsync(bindingContext);

            if (!bindingContext.Result.IsModelSet)
            {
                return;
            }

            if (_type == typeof(DateTime))
            {
                var dateTime = (DateTime)bindingContext.Result.Model;
                bindingContext.Result = ModelBindingResult.Success(Clock.Normalize(dateTime));
            }
            else
            {
                var dateTime = (DateTime?)bindingContext.Result.Model;
                if (dateTime != null)
                {
                    bindingContext.Result = ModelBindingResult.Success(Clock.Normalize(dateTime.Value));
                }
            }
        }
    }
}
