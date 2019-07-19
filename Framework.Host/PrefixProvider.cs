using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace Framework.Host
{
    public class PrefixProvider : DefaultDirectRouteProvider
    {
        private readonly string _centralizedPrefix;

        public PrefixProvider(string centralizedPrefix)
        {
            _centralizedPrefix = centralizedPrefix;
        }

        protected override string GetRoutePrefix(HttpControllerDescriptor controllerDescriptor)
        {
            var existingPrefix = base.GetRoutePrefix(controllerDescriptor);
            if (existingPrefix == null) return _centralizedPrefix;

            return $"{_centralizedPrefix}/{existingPrefix}";
        }
    }
}