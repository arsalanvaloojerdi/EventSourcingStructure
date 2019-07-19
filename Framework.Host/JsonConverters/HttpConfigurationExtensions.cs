using System.Web.Http;
using System.Xml;
using Newtonsoft.Json.Serialization;
using Formatting = Newtonsoft.Json.Formatting;

namespace Framework.Host.JsonConverters
{
    public static class HttpConfigurationExtensions
    {
        public static void UseJsonFormatters(this HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = (Formatting) System.Xml.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new JsonDateTimeConverter());
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new KeyValueConverter());
        }
    }
}
