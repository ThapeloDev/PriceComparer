using HtmlAgilityPack;
using System;
using System.Globalization;

namespace PriceComparer.Scrapers.Common
{
    public static class HtmlAttributeCollectionExtensions
    {
        public static T GetValue<T>(this HtmlAttributeCollection attributes, string key)
        {
            return attributes.GetValue<T>(key, new CultureInfo("nl-NL"));
        }

        public static T GetValue<T>(this HtmlAttributeCollection attributes, string key, IFormatProvider formatProvider)
        {
            var value = attributes[key]?.Value;

            if (string.IsNullOrEmpty(value))
                return default(T);

            T convertedValue = (T)Convert.ChangeType(value, typeof(T), formatProvider);

            return convertedValue;
        }
    }
}
