using System;

namespace Framework.Core.Extentions
{
    public static class LongExtension
    {
        public static string ToCommaSplittedString(this long number)
        {
            return number.ToString("#,##0");
        }

        public static bool IsNegative(this long number)
        {
            return number < 0;
        }

        public static bool IsGreaterThanOrEqualTo(this long number, long numberToCompare)
        {
            return number >= numberToCompare;
        }

        public static bool IsLessThan(this long number, long numberToCompare)
        {
            return number < numberToCompare;
        }
    }

    public static class DecimalExtension
    {
        public static string ToCommaSplittedString(this decimal number)
        {
            return number.ToString("#,##0");
        }

    }

    public static class NumberHelper
    {
        private static string[] yakan = new string[10] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        private static string[] dahgan = new string[10] { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        private static string[] dahyek = new string[10] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        private static string[] sadgan = new string[10] { "", "یکصد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
        private static string[] basex = new string[5] { "", "هزار", "میلیون", "میلیارد", "تریلیون" };
        private static string getnum3(int num2)
        {
            int num3 = (num2 < 0 ? num2 * -1 : num2);


            string s = "";
            int d3, d12;
            d12 = num3 % 100;
            d3 = num3 / 100;
            if (d3 != 0)
                s = sadgan[d3] + " و ";
            if ((d12 >= 10) && (d12 <= 19))
            {
                s = s + dahyek[d12 - 10];
            }
            else
            {
                int d2 = d12 / 10;
                if (d2 != 0)
                    s = s + dahgan[d2] + " و ";
                int d1 = d12 % 10;
                if (d1 != 0)
                    s = s + yakan[d1] + " و ";
                s = s.Substring(0, s.Length - 3);
            };
            return s;
        }
        public static string num2str(string snum)
        {
            string stotal = "";
            if (snum == "")
                return "صفر";
            if (snum == "0")
            {
                return yakan[0];
            }
            else
            {
                snum = snum.PadLeft(((snum.Length - 1) / 3 + 1) * 3, '0');
                int L = snum.Length / 3 - 1;
                for (int i = 0; i <= L; i++)
                {
                    int b = int.Parse(snum.Substring(i * 3, 3));
                    if (b != 0)
                        stotal = stotal + getnum3(b) + " " + basex[L - i] + " و ";
                }
                stotal = stotal.Substring(0, stotal.Length - 3);
            }
            return stotal;
        }
    }

    public static class RoundAmount
    {
        public static decimal KasreHezarRial(this decimal totalAmount)
        {

            Console.WriteLine("totalAmount :" + totalAmount);
            decimal result = totalAmount;
            var hezarRial = (totalAmount <= 0 ? 0 : (totalAmount - (result % 1000)));//درصورتی که  کل مبلغ  مانده صفر یا منفی باشد،کسر هزار ریال ندارد.
            Console.WriteLine("hezarRial :" + hezarRial);
            return hezarRial;
        }
    }

}