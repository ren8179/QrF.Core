using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Json
{
    public static class JsonExtensions
    {
        /// <summary>
        /// Converts given object to JSON string.
        /// </summary>
        /// <returns></returns>
        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new BaseCamelCasePropertyNamesContractResolver();
            }
            else
            {
                options.ContractResolver = new ContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }

            return JsonConvert.SerializeObject(obj, options);
        }
    }
}
