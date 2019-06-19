using LogParser.DAL.Configure;

using Microsoft.EntityFrameworkCore;

namespace LogParser.DAL.Context
{
    public static class ModelBuilderExtensions
    {
        public static void Configure(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new ProtocolConfiguration());
            modelBuilder.ApplyConfiguration(new ProtocolVersionConfiguration());
            modelBuilder.ApplyConfiguration(new RequestUrlConfiguration());
            modelBuilder.ApplyConfiguration(new RestMethodConfiguration());
            modelBuilder.ApplyConfiguration(new RestStatusCodeConfiguration());
            modelBuilder.ApplyConfiguration(new RouteConfiguration());
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
        }
    }
}