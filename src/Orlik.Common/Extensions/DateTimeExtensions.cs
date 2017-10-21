using System;

namespace Orlik.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsGreaterThanToday(this DateTime date)
            => date > DateTime.UtcNow;

        public static bool IsNotGreaterThanToday(this DateTime date)
            => !date.IsGreaterThanToday();
    }
}
