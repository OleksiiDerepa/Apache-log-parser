using LogParser.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogParser.DAL.Configure
{
    public class RestStatusCodeConfiguration : IEntityTypeConfiguration<RestStatusCode>
    {
        public void Configure(EntityTypeBuilder<RestStatusCode> builder)
        {
            builder.Property(x => x.Number).IsRequired();
            builder.HasIndex(x => x.Number).IsUnique();
        }
    }
}