using Framework.Application.Contracts;
using Newtonsoft.Json;

namespace EventSource.Structure.Infrastructure.Logging.Extentions
{
    public static class CommandExtentions
    {
        public static string ToSerialize(this ICommandBase command)
        {
            return JsonConvert.SerializeObject(command);
        }
    }
}