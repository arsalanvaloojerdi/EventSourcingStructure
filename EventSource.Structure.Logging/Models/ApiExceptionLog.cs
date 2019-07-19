using System;

namespace EventSource.Structure.Infrastructure.Logging.Models
{
    public class ApiExceptionLog : BaseLog
    {
        public ApiExceptionLog(string controllerName, string actionName, Exception exception)
            : base(exception, false)
        {
            this.ControllerName = controllerName;
            this.ActionName = actionName;
        }

        /// <summary>
        /// نام کنترلر
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// نام اکشن
        /// </summary>
        public string ActionName { get; set; }

        // FOR ORM !
        private ApiExceptionLog() { }
    }
}