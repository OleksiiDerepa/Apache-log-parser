using LogParser.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogParser.DAL.Configure
{
    public class RestMethodConfiguration : IEntityTypeConfiguration<RestMethod>
    {
        public void Configure(EntityTypeBuilder<RestMethod> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}