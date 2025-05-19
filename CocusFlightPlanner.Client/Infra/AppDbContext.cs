using CocusFlightPlanner.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CocusFlightPlanner.Application.Infra
{
    public class AppDbContext : DbContext
    {
        public DbSet<AircraftType> AircraftTypes { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<TravelRoute> Routes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<AircraftType>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<TravelRoute>().HasQueryFilter(p => !p.Deleted);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(x => !x.IsOwnership && x.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(x => x.DeleteBehavior = DeleteBehavior.Restrict);
            }

            modelBuilder.Entity<AircraftType>().HasData(
                new AircraftType
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Boeing 737 Max",
                    Mtow = 50,
                    CruiseFuelBurn = 7f,
                    CreatedAt = DateTimeOffset.Parse("2024-01-01T00:00:00+00:00"),
                    Deleted = false
                },
                new AircraftType
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Boeing 747",
                    Mtow = 390,
                    CruiseFuelBurn = 10f,
                    CreatedAt = DateTimeOffset.Parse("2024-01-01T00:00:00+00:00"),
                    Deleted = false
                },
                new AircraftType
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Airbus A320",
                    Mtow = 45,
                    CruiseFuelBurn = 8f,
                    CreatedAt = DateTimeOffset.Parse("2024-01-01T00:00:00+00:00"),
                    Deleted = false
                },
                new AircraftType
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Name = "Boeing 777",
                    Mtow = 350,
                    CruiseFuelBurn = 9f,
                    CreatedAt = DateTimeOffset.Parse("2024-01-01T00:00:00+00:00"),
                    Deleted = false
                },
                new AircraftType
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Name = "Gulfstream G650",
                    Mtow = 100,
                    CruiseFuelBurn = 5f,
                    CreatedAt = DateTimeOffset.Parse("2024-01-01T00:00:00+00:00"),
                    Deleted = false
                }
            );        

            base.OnModelCreating(modelBuilder);
        }
    }
}