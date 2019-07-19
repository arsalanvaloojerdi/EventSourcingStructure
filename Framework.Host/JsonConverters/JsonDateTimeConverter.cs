using System;
using Framework.Core.Extentions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Framework.Host.JsonConverters
{
    public class JsonDateTimeConverter : DateTimeConverterBase
    {
        //...
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).FaDate());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return reader.Value == null || string.IsNullOrEmpty(reader.Value.ToString())
                    ? (object)null
                    : reader.Value.ToString().ConvertToDate();
            }
            catch (Exception)
            {
                return reader.Value;
            }
        }
    }
}