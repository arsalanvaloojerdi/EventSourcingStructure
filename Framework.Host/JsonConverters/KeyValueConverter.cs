using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Framework.Host.JsonConverters
{
    public class KeyValueConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            List<KeyValuePair<string, object>> list = value as List<KeyValuePair<string, object>>;
            writer.WriteStartArray();
            foreach (var item in list)
            {
                writer.WriteStartObject();
                writer.WritePropertyName(item.Key);
                writer.WriteValue(item.Value);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var list = new List<KeyValuePair<string, object>>();
            if (reader.TokenType == JsonToken.StartArray)
            {
                int startDepth = reader.Depth;
                while (reader.Read())
                {
                    if ((reader.Depth == startDepth) && (reader.TokenType == JsonToken.EndArray)) break;

                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        KeyValuePair<string, object> kvp = GetKeyValueFromJsonObject(reader);
                        list.Add(kvp);
                    }
                    else
                    {
                        throw new NotSupportedException(String.Format("Unexpected JSON token '{0}' while reading special Dictionary", reader.TokenType));
                    }
                }
            }
            return list;
        }

        private KeyValuePair<string, object> GetKeyValueFromJsonObject(JsonReader reader)
        {
            string key = null;
            object value = null;
            int startDepth = reader.Depth;
            while (reader.Read())
            {
                if ((reader.TokenType == JsonToken.EndObject) && (reader.Depth == startDepth)) break;

                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string propName = reader.Value as string;
                    if (propName == null) continue;
                    key = propName;
                    value = reader.ReadAsString();
                }
            }
            return new KeyValuePair<string, object>(key, value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<KeyValuePair<string, object>>);
        }
    }
}