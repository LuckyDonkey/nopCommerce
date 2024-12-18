using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Nop.Plugin.Misc.WatermarkPro;
internal class CommonSettingsConvertor : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        string? src = value as string;
        if (src != null)
        {
            return JsonConvert.DeserializeObject<CommonSettings>(src);
        }
        return base.ConvertFrom(context, culture, value);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string))
        {
            return JsonConvert.SerializeObject(value);
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}

public class NoTypeConverterJsonConverter<T> : JsonConverter
{
    static readonly IContractResolver _resolver = new NoTypeConverterContractResolver();

    class NoTypeConverterContractResolver : DefaultContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            if (typeof(T).IsAssignableFrom(objectType))
            {
                var contract = this.CreateObjectContract(objectType);
                contract.Converter = null; // Also null out the converter to prevent infinite recursion.
                return contract;
            }
            return base.CreateContract(objectType);
        }
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(T).IsAssignableFrom(objectType);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        return JsonSerializer.CreateDefault(new JsonSerializerSettings { ContractResolver = _resolver }).Deserialize(reader, objectType);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        JsonSerializer.CreateDefault(new JsonSerializerSettings { ContractResolver = _resolver }).Serialize(writer, value);
    }
}