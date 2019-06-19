using System;
using System.Globalization;

namespace LogParser.Common.Extensions
{
    public static class DateExtensions
    {
        public static string Format = "dd/MMM/yyyy:H:mm:ss zzz";

        public static DateTimeOffset ConvertToDateTimeOffset(this string dateString) =>
            DateTimeOffset.ParseExact(dateString, Format, CultureInfo.InvariantCulture);
    }
}