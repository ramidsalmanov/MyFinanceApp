using System.Reflection;
using MyFinance.Core.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MyFinance.WebApi.Helpers.Json;

public class CategoryContractResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);

        if (property.DeclaringType == typeof(CategoryDto))
        {
            if (property.PropertyName != null && property.PropertyName.Equals("Label", StringComparison.Ordinal))
            {
                property.PropertyName = "name";
            }
        }

        return property;
    }
}