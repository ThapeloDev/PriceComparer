using PriceComparer.Domain;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PriceComparer.Scrapers
{
    public static class StringExtensions
    {
        public static string GetLastWord(this string value)
        {
            return value
                .RemoveNonAlhaNumericCharacters()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Last();
        }

        public static string GetSecondLastWord(this string value)
        {
            return value
                .RemoveNonAlhaNumericCharacters()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Reverse()
                .Skip(1)
                .Take(1)
                .FirstOrDefault();
        }

        public static string RemoveNonAlhaNumericCharacters(this string value)
        {
            if (value.IsEmpty())
                return value;

            Regex rgx = new Regex("(\\S.*)");
            return rgx.Match(value).Groups[1].Value;
        }
    }
}
