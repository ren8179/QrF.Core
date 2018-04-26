using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using QrF.ABP.Reflections;
using QrF.ABP.Timing;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace QrF.ABP.Json
{
    public class BaseCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            ModifyProperty(member, property);

            return property;
        }

        protected virtual void ModifyProperty(MemberInfo member, JsonProperty property)
        {
            if (property.PropertyType != typeof(DateTime) && property.PropertyType != typeof(DateTime?))
            {
                return;
            }

            if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableDateTimeNormalizationAttribute>(member) == null)
            {
                property.Converter = new DateTimeConverter();
            }
        }
    }
}
