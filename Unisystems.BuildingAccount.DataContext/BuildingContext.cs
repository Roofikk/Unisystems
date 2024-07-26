using Microsoft.EntityFrameworkCore;

namespace Unisystems.BuildingAccount.DataContext;

public class BuildingContext : DbContext
{
    public DbSet<Building> Buildings { get; set; }

    public BuildingContext()
        : base()
    {
    }

    public BuildingContext(DbContextOptions<BuildingContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=unisystem_buildings;Username=postgres;Password=root1234;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Building>(e =>
        {
            e.HasIndex(x => x.Name);
            e.HasIndex(x => x.Address);
        });
    }
}
