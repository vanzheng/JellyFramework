using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jelly.Helpers
{
    public static class DateTimeUtils
    {
        private static readonly string[] fuzzyHours = new string[] { 
        "midnight", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "noon", "one", "two", "three", 
        "four", "five", "six", "seven", "eight", "nine", "ten", "eleven"
     };
        private static readonly string[] fuzzyMinutes = new string[] { "five", "ten", "a quarter", "twenty", "twenty five", "half" };

        private static readonly string[] _suffixes = new string[] { "th", "st", "nd", "rd" };

        /// <summary>
        /// Converts a number value into a string that represents the number
        /// expressed in whole kilobytes. This is a format similar to the
        /// Windows Explorer "Size" column.
        /// </summary>
        public static string FileSizeToStringKB(long fileSize)
        {
            return string.Format("{0:n0} KB", Math.Ceiling((double)fileSize / 1024));
        }

        public static string DateTimeToWords(DateTime date)
        {
            return DateTimeToWords(date, DateTime.Now);
        }

        public static string DateTimeToWords(DateTime dateTime, DateTime currentDate)
        {
            string result;
            TimeSpan t1 = new TimeSpan(currentDate.Ticks);
            TimeSpan t2 = new TimeSpan(dateTime.Ticks);
            int daysElapsed = t1.Days - t2.Days;
            if (daysElapsed < -7 || daysElapsed >= 14)
                result = DateToWords(dateTime, currentDate);
            else if (daysElapsed == 0)
                result = "this " + GetPeriod(dateTime.Hour);
            else
                result = DateToWords(dateTime, currentDate) + " " + GetPeriod(dateTime.Hour);

            return (result + " at " + TimeToWords(dateTime));
        }

        public static string DateToWords(DateTime date)
        {
            return DateToWords(date, DateTime.Now);
        }

        public static string DateToWords(DateTime date, DateTime currentDate)
        {
            TimeSpan t1 = new TimeSpan(currentDate.Ticks);
            TimeSpan t2 = new TimeSpan(date.Ticks);
            int daysElapsed = t1.Days - t2.Days;

            if (daysElapsed < -1 && daysElapsed >= -7)
                return ("next " + date.ToString("dddd"));

            if (daysElapsed == -1)
                return "tomorrow";

            if (daysElapsed == 0)
                return "today";

            if (daysElapsed == 1)
                return "yesterday";

            if (daysElapsed > 1 && daysElapsed < 7)
                return date.ToString("dddd");

            if (daysElapsed >= 7 && daysElapsed < 14)
                return "last " + date.ToString("dddd");

            return
              (date.ToString("MMMM") + " " + GetOrdinal(date.Day) +
               ((date.Year != currentDate.Year) ? (" " + date.ToString("yyyy")) : string.Empty));
        }

        public static string GetOrdinal(int value)
        {
            int tenth = value % 10;

            if (tenth >= _suffixes.Length)
            {
                return _suffixes[0];
            }
            else
            {
                // special case for 11, 12, 13
                int hundredth = value % 100;
                if (hundredth >= 11 && hundredth <= 13)
                    return _suffixes[0];

                return _suffixes[tenth];
            }
        }

        private static string GetPeriod(int hour)
        {
            if (hour > 18)
                return "evening";

            if (hour > 12)
                return "afternoon";

            if (hour > 3)
                return "morning";

            return "night";
        }

        public static string TimeToWords(DateTime time)
        {
            string result;
            int minutes = time.Minute;
            int hours = time.Hour;
            bool toHour = false;
            int remainder = time.Minute % 5;

            if (remainder < 3)
                minutes -= remainder;
            else
                minutes += 5 - remainder;

            if (minutes > 30)
            {
                hours = (hours + 1) % 24;
                minutes = 60 - minutes;
                toHour = true;
            }

            if (minutes != 0)
                result = fuzzyMinutes[minutes / 6] + " " + (toHour ? "to" : "past") + " " + fuzzyHours[hours];
            else
                result = fuzzyHours[hours] + ((hours != 0 && hours != 12) ? " o'clock" : string.Empty);

            if (hours > 0 && hours < 12)
                return result + " am";

            if (hours > 12)
                result = result + " pm";

            return result;
        }
    }
}
