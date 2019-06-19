using System;

namespace LogParser.DAL.Entities
{
    public class RequestUrl : IEntity
    {
        public long Id { get; set; }
        public string Params { get; set; }
        public DateTimeOffset Date { get; set; }

        public Address IpAddress { get; set; }
        public long IpAddressId { get; set; }

        public Route Route { get; set; }
        public long RouteId { get; set; }

        public RestMethod Method { get; set; }  
        public long MethodId { get; set; }

        public Protocol Protocol { get; set; }
        public long ProtocolId { get; set; }

        public ProtocolVersion ProtocolVersion { get; set; }
        public long ProtocolVersionId { get; set; }

        public RestStatusCode StatusCode { get; set; }
        public long StatusCodeId { get; set; }
    }
}