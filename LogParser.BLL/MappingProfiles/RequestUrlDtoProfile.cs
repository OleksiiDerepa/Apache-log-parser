using AutoMapper;

using LogParser.DAL.Entities;
using LogParser.DataModels.Models;

namespace LogParser.BLL.MappingProfiles
{
    public class RequestUrlDtoProfile : Profile
    {
        public RequestUrlDtoProfile()
        {
            CreateMap<RequestUrlDto, RequestUrl>();

            CreateMap<RequestUrl, RequestUrlDto>()
                .ForMember(d => d.ProtocolVersion, o => o.MapFrom(s => s.ProtocolVersion.Version))
                .ForMember(d => d.IpAddress, o => o.MapFrom(s => s.IpAddress.Ip))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.IpAddress.Country.Name))
                .ForMember(d => d.Latitude, o => o.MapFrom(s => s.IpAddress.Latitude))
                .ForMember(d => d.Longitude, o => o.MapFrom(s => s.IpAddress.Longitude))
                .ForMember(d => d.Route, o => o.MapFrom(s => s.Route.Name))
                .ForMember(d => d.Method, o => o.MapFrom(s => s.Method.Name))
                .ForMember(d => d.Protocol, o => o.MapFrom(s => s.Protocol.Name))
                .ForMember(d => d.Size, o => o.MapFrom(s => s.ResponseSize))
                .ForMember(d => d.StatusCode, o => o.MapFrom(s => s.StatusCode.Number));
        }
    }
}