namespace MyTelescope.Api.DataLayer.Context
{
    using Microsoft.EntityFrameworkCore;
    using SolarSystem.Models.CelestialObject;
    using System.Linq;

    public class MyTelescopeContext : DbContext
    {
        private string ConnectionString { get; }

        public MyTelescopeContext()
            : this("data source=DESKTOP-VJ9ESHG\\SQLEXPRESS;Database=MyTelescope;Persist Security Info=True;Integrated Security=SSPI;MultipleActiveResultSets=true;")
        {
        }

        public MyTelescopeContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                ConnectionString,
                o =>
                {
                    o.CommandTimeout(30);
                    o.EnableRetryOnFailure();
                });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CelestialObjectType
            modelBuilder.Entity<CelestialObjectType>().HasKey(x => new { x.Id });
            modelBuilder.Entity<CelestialObjectType>().HasMany(x => x.CelestialObjects).WithOne(x => x.CelestialObjectType).HasForeignKey(x => x.CelestialObjectTypeId);
            modelBuilder
                .Entity<CelestialObjectType>()
                .HasIndex(x => new { x.Code }).IsUnique();

            // CelestialObjectType
            modelBuilder.Entity<CelestialObject>().HasKey(x => new { x.Id });
            modelBuilder
                .Entity<CelestialObject>()
                .HasIndex(x => new { x.Code }).IsUnique();
            modelBuilder.Entity<CelestialObject>().HasMany(x => x.CelestialObjectPositions).WithOne(x => x.CelestialObject).HasForeignKey(x => x.CelestialObjectId);

            // CelestialObjectType
            modelBuilder.Entity<CelestialObjectPosition>().HasKey(x => new { x.Id });
            modelBuilder
                .Entity<CelestialObjectPosition>()
                .HasIndex(x => new { x.CelestialObjectId, x.ReferenceDate }).IsUnique();

            foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<CelestialObjectType> CelestialObjectType { get; set; }

        public DbSet<CelestialObject> CelestialObject { get; set; }

        public DbSet<CelestialObjectPosition> CelestialObjectPosition { get; set; }
    }
}