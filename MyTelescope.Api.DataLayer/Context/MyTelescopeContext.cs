namespace MyTelescope.Api.DataLayer.Context
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using SolarSystem.Models.CelestialObject;

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
            modelBuilder.Entity<CelestialObjectTypeModel>().HasKey(x => new { x.Id });
            modelBuilder.Entity<CelestialObjectTypeModel>().HasMany(x => x.CelestialObjects).WithOne(x => x.CelestialObjectType).HasForeignKey(x => x.CelestialObjectTypeId);
            modelBuilder
                .Entity<CelestialObjectTypeModel>()
                .HasIndex(x => new { x.Code }).IsUnique();

            // CelestialObjectType
            modelBuilder.Entity<CelestialObjectModel>().HasKey(x => new { x.Id });
            modelBuilder
                .Entity<CelestialObjectModel>()
                .HasIndex(x => new { x.Code }).IsUnique();
            modelBuilder.Entity<CelestialObjectModel>().HasMany(x => x.CelestialObjectPositions).WithOne(x => x.CelestialObject).HasForeignKey(x => x.CelestialObjectId);

            // CelestialObjectType
            modelBuilder.Entity<CelestialObjectPositionModel>().HasKey(x => new { x.Id });
            modelBuilder
                .Entity<CelestialObjectPositionModel>()
                .HasIndex(x => new { x.CelestialObjectId, x.ReferenceDate }).IsUnique();

            foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<CelestialObjectTypeModel> CelestialObjectType { get; set; }

        public DbSet<CelestialObjectModel> CelestialObject { get; set; }

        public DbSet<CelestialObjectPositionModel> CelestialObjectPosition { get; set; }
    }
}