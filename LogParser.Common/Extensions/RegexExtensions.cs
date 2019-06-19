using System.Text.RegularExpressions;

using static LogParser.Common.ConstantValues;

namespace LogParser.Common.Extensions
{
    public static class RegexExtensions
    {
        public static bool IsCorrectRecord(this string data)
        {
            if (!RequestMatch.IsMatch(data))
            {
                return false;
            }

            string matchedValue = RequestMatch.Match(data).GetValue(HostNameOrAddress);

            return matchedValue.IsIpv4()
                || matchedValue.IsIpv6()
                || matchedValue.IsHost();
        }

        public static bool IsHost(this string data)
        {
            var result =  HostMatch.IsMatch(data);
            return result;
        }

        public static bool IsIpv4(this string data)
        {
            var result =  Ipv4Match.IsMatch(data);
            return result;
        }
        public static bool IsIpv6(this string data)
        {
            var result =  Ipv6Match.IsMatch(data);
            return result;
        }

        public static string GetValue(this Match match, string property) =>
            match.Groups[property].Value;
    }
}