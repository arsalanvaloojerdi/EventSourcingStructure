using System;

namespace EventSource.Structure.Infrastructure.Logging.Models
{
    public abstract class BaseLog
    {
        protected BaseLog(Exception exception, bool operationSuccess)
        {
            this.Id = Guid.NewGuid();
            this.Exception = exception?.ToString();
            this.StackTrace = exception?.StackTrace;
            this.OperationSuccess = operationSuccess;
            this.Date = DateTime.Now;
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// خطا ( در صورت رخ دادن )
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// StackTrace
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// تاریخ لاگ
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// آیا عملیات موفق بوده است ?
        /// </summary>
        public bool OperationSuccess { get; set; }

        // FOR ORM !
        protected BaseLog() { }
    }
}