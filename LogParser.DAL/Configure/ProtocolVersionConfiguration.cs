using LogParser.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogParser.DAL.Configure
{
    public class ProtocolVersionConfiguration : IEntityTypeConfiguration<ProtocolVersion>
    {
        public void Configure(EntityTypeBuilder<ProtocolVersion> builder)
        {
            builder.Property(x => x.Version).IsRequired();
            builder.HasIndex(x => x.Version).IsUnique();
        }
    }
}