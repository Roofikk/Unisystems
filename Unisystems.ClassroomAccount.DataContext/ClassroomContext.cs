using Microsoft.EntityFrameworkCore;
using Unisystem.ClassroomAccount.DataContext.Entities;

namespace Unisystem.ClassroomAccount.DataContext;

public class ClassroomContext : DbContext
{
    public DbSet<Building> Buildings { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }

    public ClassroomContext()
        : base()
    {
    }

    public ClassroomContext(DbContextOptions<ClassroomContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=unisystem_classrooms;Username=postgres;Password=root1234",
                npgOptions =>
                {
                    npgOptions.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
                    npgOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        null);
                });
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Classroom>(e =>
        {
            e.HasKey(c => c.ClassroomId);
            e.Property(c => c.ClassroomId).ValueGeneratedOnAdd();

            e.HasOne(c => c.Building)
                .WithMany(b => b.Classrooms)
                .HasForeignKey(c => c.BuildingId);

            e.HasOne(c => c.RoomType)
                .WithMany(t => t.Classrooms)
                .HasForeignKey(c => c.RoomTypeId);

            e.HasIndex(c => c.Name).HasDatabaseName("IX_Classroom_Name");
        });

        modelBuilder.Entity<RoomType>(e =>
        {
            e.HasKey(t => t.KeyName);
            e.Property(t => t.KeyName).ValueGeneratedNever();
        });

        modelBuilder.Entity<Building>(e =>
        {
            e.ToTable("PartialBuildings");

            e.HasKey(b => b.BuildingId);
            e.Property(b => b.BuildingId).ValueGeneratedNever();

            e.HasIndex(b => b.Name).HasDatabaseName("IX_Building_Name");
        });
    }
}
