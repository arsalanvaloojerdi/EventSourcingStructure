using System;
using System.Net;

namespace Framework.Host.ExceptionFilter
{
    public class ExceptionInfoBuilder
    {
        private Type _exceptionType;
        private string _message;
        private HttpStatusCode _statusCode;

        public ExceptionInfoBuilder()
        {
            _exceptionType = typeof(Exception);
            _message = "متاسفانه خطای سیستمی رخ داده است";
            _statusCode = HttpStatusCode.InternalServerError;
        }

        public ExceptionInfoBuilder WithType(Type exceptionType)
        {
            this._exceptionType = exceptionType;

            return this;
        }

        public ExceptionInfoBuilder WithMessage(string message)
        {
            this._message = message;

            return this;
        }

        public ExceptionInfoBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            this._statusCode = statusCode;

            return this;
        }

        public ExceptionInfo Build()
        {
            return new ExceptionInfo(_exceptionType, _message, _statusCode);
        }
    }
}