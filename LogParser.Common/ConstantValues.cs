using System.Text.RegularExpressions;

namespace LogParser.Common
{
    public static class ConstantValues
    {
        public const string HostNameOrAddress = "domain";
        public const string Date = "date";
        public const string Method = "method";
        public const string Route = "endpoint";
        public const string Params = "params";
        public const string Protocol = "protocol";
        public const string ProtocolVersion = "protocolnumber";
        public const string StatusCode = "statuscode";
        public const string ResponseSize = "responsesize";

        public static Regex RequestMatch = new Regex(@"(?<domain>^[\w\d\.\-]*)\s-\s-\s\[(?<date>.*?)\]\s""(?<request>(?<method>\w{3,7})\s(((?<endpoint>[/\w]*)(\?(?<params>[\w\D]*?))?)|\*))\s(?<protocol>\w{4,5})/(?<protocolnumber>\d.\d)""\s(?<statuscode>\d{3})\s(?<responsesize>\d*)$", RegexOptions.Compiled | RegexOptions.Singleline);
        public static Regex Ipv4Match = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", RegexOptions.Compiled | RegexOptions.Singleline);
        public static Regex Ipv6Match = new Regex(
            @"^(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))$",
            RegexOptions.Compiled | RegexOptions.Singleline);
        public static Regex HostMatch = new Regex(@"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$", RegexOptions.Compiled | RegexOptions.Singleline);
    }
}
