using System;

namespace Framework.Core.Extentions
{
    public static class DateTimeExtentions
    {
        public static bool IsPast(this DateTime date)
        {
            return date <= DateTime.Now;
        }

        public static bool IsBefore(this DateTime target, DateTime date)
        {
            return target < date;
        }

        public static bool IsBeforeNow(this DateTime target)
        {
            return target < DateTime.Now;
        }

        public static bool IsBeforeToday(this DateTime target)
        {
            return target.Date < DateTime.Now.Date;
        }
    }
}