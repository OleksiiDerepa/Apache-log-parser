using LogParser.Common;
using LogParser.Common.Extensions;

namespace LogParser.DataModels.Models
{
    public class RowLogItem
    {
        public RowLogItem(string data)
        {
            var match = ConstantValues.RequestMatch.Match(data);
            this.Date = match.GetValue(ConstantValues.Date);
            this.HostNameOrAddress = match.GetValue(ConstantValues.HostNameOrAddress);
            this.Route = match.GetValue(ConstantValues.Route);
            this.Method = match.GetValue(ConstantValues.Method);
            this.Params = match.GetValue(ConstantValues.Params);
            this.Protocol = match.GetValue(ConstantValues.Protocol);
            this.ProtocolVersion = match.GetValue(ConstantValues.ProtocolVersion);
            this.ResponseSize = match.GetValue(ConstantValues.ResponseSize);
            this.StatusCode = match.GetValue(ConstantValues.StatusCode);
        }
        public IpGeoLocation IpGeoLocation { get; set; }
        public string HostNameOrAddress { get; set; }
        public string IP { get; set; }
        public string Date { get; set; }
        public string Method { get; set; }
        public string Route { get; set; }
        public string Params { get; set; }
        public string Protocol { get; set; }
        public string ProtocolVersion { get; set; }
        public string StatusCode { get; set; }
        public string ResponseSize { get; set; }
    }
}