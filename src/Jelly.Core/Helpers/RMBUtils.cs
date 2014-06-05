using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Jelly.Helpers
{
    /// <summary> 
    /// CMB utility class.
    /// </summary> 
    public static class RMBUtils
    {
        private readonly static string[] CapitalDigits = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
        private readonly static string CapitalUnits = "万仟佰拾亿仟佰拾万仟佰拾元角分";

        /// <summary> 
        /// Cast lowercase digital to capital CNY.
        /// </summary> 
        /// <param name="number">The currency number.</param>
        /// <returns>The capital CNY.</returns>
        public static string ToCapitalCNY(decimal number)
        {
            ExceptionManager.ThrowArgumentExceptionIfMeet(number < 0, "The currency number can not be negative.", "number");
            ExceptionManager.ThrowArgumentExceptionIfMeet(Decimal.Truncate(number).ToString().Length > 13, "The integer number is great than 13 digits", "number");

            // Adjust the currency number two decimal point.
            decimal fixedNumber = Decimal.Round(number, 2);

            if (fixedNumber == 0M)
            {
                return "零元整";
            }

            int index;
            long money = decimal.ToInt64(fixedNumber * 100);
            string currency = money.ToString();
            int len = currency.Length;
            string actualCapitalUnits = CapitalUnits.Substring(15 - len);
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < len; i++) 
            {
                index = int.Parse(currency[i].ToString());

                // In ”元“，”万“，”亿“ position.
                if (len - i == 3 || len - i == 7 || len - i == 11)
                {
                    if (index == 0)
                    {
                        builder.Append(actualCapitalUnits[i]);
                    }
                    else 
                    {
                        builder.Append(CapitalDigits[index] + actualCapitalUnits[i]);                        
                    }
                }
                else
                {
                    if (index == 0)
                    {
                        builder.Append(CapitalDigits[index]);
                    }
                    else
                    {
                        builder.Append(CapitalDigits[index] + actualCapitalUnits[i]);
                    }
                }
            }

            string captialMoney = builder.ToString();

            // Merge one more "Zero" to one "Zero";
            Regex regex = new Regex("零+");
            captialMoney = regex.Replace(captialMoney, "零");

            // Clean up "零" in preceding ”元“，”万“，”亿“
            int yuanIndex = captialMoney.IndexOf("元");
            captialMoney = CleanupZero(captialMoney, yuanIndex);

            int wanIndex = captialMoney.IndexOf("万");
            captialMoney = CleanupZero(captialMoney, wanIndex);

            int yiIndex = captialMoney.IndexOf("亿");
            captialMoney = CleanupZero(captialMoney, yiIndex);
            
            // Remove the last two units while the last two are both zero.
            if (currency.EndsWith("00", StringComparison.OrdinalIgnoreCase)) 
            {
                captialMoney = captialMoney.Remove(captialMoney.IndexOf("元") + 1);
                captialMoney = captialMoney + "整";
            }

            return captialMoney;
        }

        private static string CleanupZero(string input, int index) 
        {
            if (index > -1 && input[index - 1].Equals('零'))
            {
                input = StringUtils.RemoveSpecifiedIndex(input, index - 1);
            }

            return input;
        }
    }
}
