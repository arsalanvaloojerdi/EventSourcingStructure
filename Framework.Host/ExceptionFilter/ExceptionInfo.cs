using System;
using System.Net;

namespace Framework.Host.ExceptionFilter
{
    public class ExceptionInfo
    {
        /// <inheritdoc />
        public ExceptionInfo(Type type, string message, HttpStatusCode statusCode)
        {
            Type = type;
            Message = message;
            StatusCode = statusCode;
        }

        public static ExceptionInfo Default => new ExceptionInfoBuilder()
            .Build();

        public Type Type { get; private set; }

        public string Message { get; private set; }

        public HttpStatusCode StatusCode { get; private set; }
    }
}