using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Framework.Host.ExceptionFilter
{
    /// <summary>
    /// اکسپشن هندلر
    /// </summary>
    public class CustomExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public HttpActionExecutedContext ActionExecutedContext { get; private set; }

        private readonly IEnumerable<ExceptionInfo> _exceptionInfos;

        public CustomExceptionHandlerAttribute(IEnumerable<ExceptionInfo> exceptionInfos)
        {
            _exceptionInfos = exceptionInfos;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            this.ActionExecutedContext = actionExecutedContext;

            HandleException();
        }

        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            this.ActionExecutedContext = actionExecutedContext;

            HandleException();

            return Task.CompletedTask;
        }

        #region PrivateMethods

        private void HandleException()
        {
            var exceptionInfo =
                _exceptionInfos.FirstOrDefault(q => q.Type == ActionExecutedContext.Exception.GetType()) ??
                ExceptionInfo.Default;

            this.ActionExecutedContext.Response = new HttpResponseMessage(exceptionInfo.StatusCode)
            {
                Content = new ObjectContent<ResponseMessage>(
                    new ResponseMessage(exceptionInfo.Message), new JsonMediaTypeFormatter(),
                    "application/json")
            };
        }

        #endregion
    }
}