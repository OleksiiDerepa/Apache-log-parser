using Newtonsoft.Json;

namespace LogParser.DataModels.Models
{
    public class IpGeoLocation
    {
        public string Ip { get; set; }

        [JsonProperty(PropertyName = "country_name")]
        public string CountryName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }}