using LogParser.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogParser.DAL.Configure
{
    public class RequestUrlConfiguration : IEntityTypeConfiguration<RequestUrl>
    {
        public void Configure(EntityTypeBuilder<RequestUrl> builder)
        {
            builder.Property(x => x.IpAddressId).IsRequired();
            builder.Property(x => x.MethodId).IsRequired();
            builder.Property(x => x.ProtocolId).IsRequired();
            builder.Property(x => x.ProtocolVersionId).IsRequired();
            builder.Property(x => x.RouteId).IsRequired();
            builder.Property(x => x.StatusCodeId).IsRequired();
        }
    }
}