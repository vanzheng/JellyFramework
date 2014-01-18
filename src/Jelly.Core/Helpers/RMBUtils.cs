using System;

namespace Jelly.Helpers
{
    /// <summary> 
    /// CMB utility class.
    /// </summary> 
    public static class RMBUtils
    {
        /// <summary> 
        /// Cast lowercase digital to capital CNY.
        /// </summary> 
        /// <param name="number">The currency number.</param>
        /// <returns>The capital CNY.</returns>
        public static string ToCapitalCNY(decimal number)
        {
            if (number < 0)
            {
                throw new Exception("The currency number can not be negative.");
            }

            if (number < 0.01m && number > 0)
            {
                throw new Exception("The currency number is less than 0.01 and great than 0, it's too small to cast to Capital CNY.");
            }

            if (number == 0)
            {
                return "��Ԫ��";
            }

            string num;
            int len;
            number = Math.Round(number, 2); 
            num = ((long)(number * 100)).ToString();
            len = num.Length;     

            if (len > 15)
            {
                throw new Exception("The currency number is great than 15 digits, the amount is overflow.");
            }

            char[] capitalDigits = { '��', 'Ҽ', '��', '��', '��', '��', '½', '��', '��', '��' };
            string capitalUnits = "��Ǫ��ʰ��Ǫ��ʰ��Ǫ��ʰԪ�Ƿ�";
            string captialMoney;
            string actualCapitalUnits = capitalUnits.Substring(15 - len);
            string[] capitalCNY = actualCapitalUnits.Split();
            char[] numChar = num.ToCharArray();

            for (int i = 0; i < len; i++)
            {
                // In the Fen(��) position.
                if (i == len - 1)
                {
                    if (numChar[len - 1] == '0')
                    {
                        capitalCNY[i] = String.Empty;
                        break;
                    }
                }

                // In the Jiao(��) postion.
                if (i == len - 2)
                {
                    if (numChar[len - 2] == '0')
                    {
                        if (numChar[len - 1] == '0')
                        {
                            capitalCNY[i] = String.Empty;
                        }
                        else
                        {
                            capitalCNY[i] = "��";
                        }
                        continue;
                    }
                }

                // In the yuan(Ԫ), ten thousand(��),one hundred million(��) unit, if number great than 0, add capital digital; 
                // othwise keep the name of unit.
                if (i == len - 3 || i == len - 7 || i == len - 11)
                {
                    if (numChar[i] != '0')
                    {
                        capitalCNY[i] = capitalDigits[numChar[i]] + capitalCNY[i];
                    }
                }
                else
                {
                    if (numChar[i] != '0')
                    {
                        capitalCNY[i] = capitalDigits[numChar[i]] + capitalCNY[i];
                    }
                    else
                    {
                        capitalCNY[i] = "��";
                    }
                }
            }

            captialMoney = String.Join(String.Empty, capitalCNY);
            captialMoney = captialMoney.Replace("������", "��");
            captialMoney = captialMoney.Replace("����", "��");

            if (captialMoney.EndsWith("Ԫ"))
            {
                captialMoney = captialMoney + "��";
            }

            return captialMoney;
        }
    }
}
