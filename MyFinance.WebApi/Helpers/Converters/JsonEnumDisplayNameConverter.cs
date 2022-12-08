using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyFinance.WebApi.Helpers.Converters;

public class JsonEnumDisplayNameConverter : JsonConverter<Enum>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }

    public override Enum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var tt = reader.GetString();
        return (Enum) Enum.Parse(typeToConvert, tt);
    }

    public override void Write(Utf8JsonWriter writer, Enum value, JsonSerializerOptions options)
    {
        var fi = value.GetType().GetField(value.ToString());
        var attribute = fi?.GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();
        writer.WriteStringValue(attribute != null ? attribute.Name : string.Empty);
    }
}