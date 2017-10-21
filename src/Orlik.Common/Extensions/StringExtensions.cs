using System;
using System.Text.RegularExpressions;

namespace Orlik.Common.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static bool Empty(this string value)
            => string.IsNullOrWhiteSpace(value);

        public static bool NotEmpty(this string value)
            => !value.Empty();

        public static bool EqualsCaseInvariant(this string value, string valueToCompare)
        {
            if (value.Empty())
                return valueToCompare.Empty();

            return !valueToCompare.Empty() && string.Equals(value, valueToCompare, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsEmail(this string value)
            => value.NotEmpty() && EmailRegex.IsMatch(value);
    }
}
