using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Framework.Core.Extentions
{
    /// <summary>
    /// تعیین معتبر بودن کد پستی
    /// </summary>
    public static class PostalCodeHelper
    {
        /// <summary>
        /// تعیین معتبر بودن کد پستی
        /// </summary>
        /// <param name="postalCode">کد پستی وارد شده</param>
        /// <returns>
        /// در صورتی که کد پستی صحیح باشد خروجی <c>true</c> و در صورتی که کد پستی اشتباه باشد خروجی <c>false</c> خواهد بود
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public static Boolean IsValidPostalCode(string postalCode)
        {
            bool result = !String.IsNullOrEmpty(postalCode);

            //در صورتی که کد پستی وارد شده طولش کمتر از 11 رقم باشد
            if (postalCode.Contains("-") && postalCode.Length != 11)
                result = false;//throw new Exception("طول کد پستی باید ده کاراکتر باشد");

            if (!postalCode.Contains("-") && postalCode.Length != 10)
                result = false;//throw new Exception("طول کد پستی باید ده کاراکتر باشد");


            //در صورتی که کد پستی ده رقم عددی نباشد
            result = Regex.Match(postalCode, @"\d{10}").Success;
            //throw new Exception("کد پستی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد پستی را صحیح وارد نمایید");

            //در صورتی که رقم‌های کد پستی وارد شده یکسان باشد
            var allDigitEqual = new List<string>()
            {
                "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777",
                "8888888888",
                "9999999999"
            };
            return !allDigitEqual.Contains(postalCode) && result;
        }
    }
}