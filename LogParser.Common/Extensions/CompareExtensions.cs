using System;

namespace LogParser.Common.Extensions
{
    public static class CompareExtensions
    {
        public static bool IsEqual(this double a, double b)
        {
            return Math.Abs(a - b) < 0.0001;
        }
        public static bool IsEqual(this string a, string b)
        {
            return string.Equals(a ,b,StringComparison.OrdinalIgnoreCase);
        }
    }
}