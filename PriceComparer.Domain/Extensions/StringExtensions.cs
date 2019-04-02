using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PriceComparer.Domain
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static T ChangeType<T>(this string value)
        {
            return value.ChangeType<T>(new CultureInfo("en-US"));
        }

        public static T ChangeType<T>(this string value, IFormatProvider formatProvider)
        {
            if (value.IsEmpty())
                return default(T);

            return (T)Convert.ChangeType(value, typeof(T), formatProvider);
        }

        public static string StripSpecialChars(this string value)
        {
            return Regex.Replace(value, "[^0-9]+", "", RegexOptions.Compiled);
        }

    }
}
