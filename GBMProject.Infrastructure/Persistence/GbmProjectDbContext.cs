using GBMProject.Core.Entities;
using GBMProject.Infrastructure.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace GBMProject.Infrastructure.Persistence;

public class GbmProjectDbContext : DbContext
{
    public GbmProjectDbContext(DbContextOptions<GbmProjectDbContext> options)
        : base(options)
    {
    }

    public DbSet<Truck> Trucks { get; private set; } = null!;
    public DbSet<Driver> Drivers { get; private set; } = null!;
    public DbSet<Delivery> Deliveries { get; private set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(TruckMapping).Assembly);
}