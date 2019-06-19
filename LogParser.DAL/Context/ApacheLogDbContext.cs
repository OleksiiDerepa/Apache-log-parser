using LogParser.DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace LogParser.DAL.Context
{
    public class ApacheLogDbContext : DbContext
    {
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Protocol> Protocols { get; set; }
        public virtual DbSet<ProtocolVersion> ProtocolVersions { get; set; }
        public virtual DbSet<RequestUrl> RequestUrls { get; set; }
        public virtual DbSet<RestMethod> RestMethods { get; set; }
        public virtual DbSet<RestStatusCode> RestStatusCodes { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public ApacheLogDbContext(DbContextOptions<ApacheLogDbContext> options) : base(options)
        {
            base.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Configure();
            modelBuilder.Seed();
        }
    }
}