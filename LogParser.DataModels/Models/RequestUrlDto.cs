using System;

namespace LogParser.DataModels.Models
{
    public class RequestUrlDto
    {
        public string Params { get; set; }
        public DateTimeOffset Date { get; set; }
        public string IpAddress { get; set; }
        public string Country { get; set; }
        public string Route { get; set; }
        public string Method { get; set; }  
        public string Protocol { get; set; }
        public string ProtocolVersion { get; set; }
        public string StatusCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}