using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Framework.Core.Extentions
{
    /// <summary>
    /// تعیین معتبر بودن شماره موبایل
    /// </summary>
    public static class MobileNoHelper
    {
        /// <summary>
        /// تعیین معتبر بودن شماره موبایل
        /// </summary>
        /// <param name="mobileNumber">شماره موبایل وارد شده</param>
        /// <returns>
        /// در صورتی که شماره موبایل صحیح باشد خروجی <c>true</c> و در صورتی که شماره موبایل اشتباه باشد خروجی <c>false</c> خواهد بود
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public static Boolean IsValidMobileNo(string mobileNumber)
        {
            bool result = !String.IsNullOrEmpty(mobileNumber);

            //در صورتی که شماره موبایل وارد شده طولش کمتر از 11 رقم باشد
            if (mobileNumber != null && mobileNumber.Length != 11)
                result = false;//throw new Exception("طول شماره موبایل باید یازده کاراکتر باشد");

            //در صورتی که شماره موبایل یازده رقم عددی نباشد
            result = Regex.Match(mobileNumber, @"\d{11}").Success;
            //throw new Exception("شماره موبایل تشکیل شده از یازده رقم عددی می‌باشد؛ لطفا شماره موبایل را صحیح وارد نمایید");

            //در صورتی که رقم‌های شماره موبایل وارد شده یکسان باشد
            var allDigitEqual = new List<string>()
            {
                "00000000000", "11111111111", "22222222222", "33333333333", "44444444444", "55555555555", "66666666666", "77777777777",
                "88888888888",
                "99999999999"
            };
            if (allDigitEqual.Contains(mobileNumber)) return false;

            return result && Regex.Match(mobileNumber, @"^(09)([0-9]{9})").Success;
        }
    }
}