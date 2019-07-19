using System;
using System.Globalization;

namespace Framework.Core.Extentions
{
    public static class PersianDateTimeHelper
    {
        public static readonly PersianCalendar PersianCalendar = new PersianCalendar();

        public static DateTime ConvertToDate(this string src)
        {
            try
            {
                string[] strArray = src.Split('/');
                int year = int.Parse(strArray[0]);
                int month = int.Parse(strArray[1]);
                int day = int.Parse(strArray[2]);
                return PersianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            catch (Exception)
            {
                throw new FormatException();
            }
        }

        public static DateTime ConvertToDateTime(this string src, string time)
        {
            try
            {
                string[] strArray = src.Split('/');
                int year = int.Parse(strArray[0]);
                int month = int.Parse(strArray[1]);
                int day = int.Parse(strArray[2]);
                return DateTime.Parse(
                    $"{PersianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0):yyyy MM dd} {time}");
            }
            catch (Exception)
            {
                throw new FormatException();
            }
        }

        public static int FaDayOfMonth(this DateTime src)
        {
            return PersianCalendar.GetDayOfMonth(src);
        }

        public static int FaMonthOfYear(this DateTime src)
        {
            return PersianCalendar.GetMonth(src);
        }

        public static string FaMonthOfYearMm(this DateTime src)
        {
            var faMonthOfYearMm = PersianCalendar.GetMonth(src);

            if (faMonthOfYearMm.ToString().Length < 2)
                return "0" + faMonthOfYearMm;

            return faMonthOfYearMm.ToString();
        }

        public static string FaStrMonthOfYear(this DateTime src)
        {
            var mon = PersianCalendar.GetMonth(src).ToString();
            switch (mon)
            {
                case "1":
                    return "فروردین";
                case "2":
                    return "اردیبهشت";
                case "3":
                    return "خرداد";
                case "4":
                    return "تیر";
                case "5":
                    return "مرداد";
                case "6":
                    return "شهریور";
                case "7":
                    return "مهر";
                case "8":
                    return "آبان";
                case "9":
                    return "آذر";
                case "10":
                    return "دی";
                case "11":
                    return "بهمن";
                case "12":
                    return "اسفند";
                default:
                    return mon;
            }
        }

        public static int FaYear(this DateTime src)
        {
            return PersianCalendar.GetYear(src);
        }

        public static DateTime FaAddMonths(this DateTime src, int value)
        {
            return PersianCalendar.AddMonths(src, value);
        }

        public static DateTime FaAddDays(this DateTime src, int days)
        {
            return PersianCalendar.AddDays(src, days);
        }

        public static string FaDate(this DateTime src)
        {
            return
                src != DateTime.MinValue ?
                    $"{PersianCalendar.GetYear(src)}/{PersianCalendar.GetMonth(src):d2}/{PersianCalendar.GetDayOfMonth(src):d2}" :
                    string.Empty;
        }

        public static string FaDateTime(this DateTime src)
        {
            return
                $"{PersianCalendar.GetYear(src)}/{PersianCalendar.GetMonth(src):d2}/{PersianCalendar.GetDayOfMonth(src):d2} {PersianCalendar.GetHour(src):d2}:{PersianCalendar.GetMinute(src):d2}";
        }

        public static string ToPersianDateTime(this DateTime src)
        {
            return
                $"{PersianCalendar.GetDayOfMonth(src):d2} {FaStrMonthOfYear(src)} {PersianCalendar.GetYear(src)} - {PersianCalendar.GetHour(src):d2}:{PersianCalendar.GetMinute(src):d2}";
        }

        public static DateTime ToDate(int year, int month, int day)
        {
            return PersianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0).Date;
        }

        public static DateTime FaStartOfMonth(this DateTime src)
        {
            int year = PersianCalendar.GetYear(src);
            int month = PersianCalendar.GetMonth(src);
            return PersianCalendar.ToDateTime(year, month, 1, 0, 0, 0, 0).Date;
        }

        public static DateTime FaStartOfYear(this DateTime src)
        {
            return FaStartOfYear(PersianCalendar.GetYear(src));
        }

        public static DateTime FaStartOfYear(int year)
        {
            return PersianCalendar.ToDateTime(year, 1, 1, 0, 0, 0, 0).Date;
        }

        public static DateTime FaEndOfYear(this DateTime src)
        {
            return FaEndOfYear(PersianCalendar.GetYear(src));
        }

        public static DateTime FaEndOfYear(int year)
        {
            int daysInMonth = PersianCalendar.GetDaysInMonth(year, 12);
            return PersianCalendar.ToDateTime(year, 12, daysInMonth, 0, 0, 0, 0).Date;
        }

        public static DateTime FaStartOfSeason(this DateTime src, int countOfMonth)
        {
            int month = (FaMonthOfYear(src) - 1) / countOfMonth * countOfMonth + 1;
            return PersianCalendar.ToDateTime(FaYear(src), month, 1, 0, 0, 0, 0);
        }

        public static DateTime FaStartOfMonth(this DateTime src, int month)
        {
            int year = PersianCalendar.GetYear(src);
            return PersianCalendar.ToDateTime(year, month, 1, 0, 0, 0, 0).Date;
        }

        public static DateTime FaEndOfMonth(this DateTime src)
        {
            int year = PersianCalendar.GetYear(src);
            int month = PersianCalendar.GetMonth(src);
            int daysInMonth = PersianCalendar.GetDaysInMonth(year, month);
            return PersianCalendar.ToDateTime(year, month, daysInMonth, 0, 0, 0, 0).Date;
        }

        public static DateTime FaEndOfMonth(this DateTime src, int month)
        {
            int year = PersianCalendar.GetYear(src);
            int daysInMonth = PersianCalendar.GetDaysInMonth(year, month);
            return PersianCalendar.ToDateTime(year, month, daysInMonth, 0, 0, 0, 0).Date;
        }

        public static string FaWeekDayName(this DateTime src)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(src.DayOfWeek);
        }

        public static DateTime FloorByMinuts(this DateTime src)
        {
            return new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0, 0);
        }


        public static string ToShortString(this TimeSpan timeSpan)
        {
            return timeSpan.Hours + ":" + timeSpan.Minutes + ":" + timeSpan.Seconds;
        }


        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

    }
}