
using System;

namespace GoldMantis.GMFxCore.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Gets a substring of a string from beginning of the string.
        /// </summary>
        public static string Left(this string str, int len)
        {
            if (str == null)
                throw new ArgumentNullException("str");
            if (str.Length < len)
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            return str.Substring(0, len);
        }
    }
}
