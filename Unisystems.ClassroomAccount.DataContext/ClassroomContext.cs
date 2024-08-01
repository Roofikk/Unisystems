using Microsoft.EntityFrameworkCore;
using Unisystems.ClassroomAccount.DataContext.Entities;

namespace Unisystems.ClassroomAccount.DataContext;

public class ClassroomContext : DbContext
{
    public DbSet<Building> Buildings { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<ColumnType> ColumnTypes { get; set; }
    public DbSet<Column> Columns { get; set; }
    public DbSet<IntColumnValue> IntColumnValues { get; set; }
    public DbSet<InputColumnValue> InputColumnValues { get; set; }
    public DbSet<DoubleColumnValue> DoubleColumnValues { get; set; }
    public DbSet<TextAreaColumnValue> TextAreaColumnValues { get; set; }

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
                "Host=localhost;Port=5432;Database=unisystems_classrooms;Username=postgres;Password=RufikRoot123321",
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
                .HasForeignKey(c => c.RoomTypeId)
                .OnDelete(DeleteBehavior.Restrict);

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

        modelBuilder.Entity<ColumnType>(e =>
        {
            e.HasKey(t => t.ColumnTypeId);
            e.Property(t => t.ColumnTypeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Column>(e =>
        {
            e.HasKey(c => c.ColumnId);
            e.Property(c => c.ColumnId).ValueGeneratedOnAdd();

            e.HasOne(c => c.ColumnType)
                .WithMany(t => t.Columns)
                .HasForeignKey(c => c.ColumnTypeId);
        });

        modelBuilder.Entity<ColumnValue>(e =>
        {
            e.UseTpcMappingStrategy();
            e.HasKey(c => new { c.ClassroomId, c.ColumnId });

            e.HasOne(c => c.Classroom)
                .WithMany(c => c.ColumnValues)
                .HasForeignKey(c => c.ClassroomId);

            e.HasOne(c => c.Column)
                .WithMany(c => c.ColumnValues)
                .HasForeignKey(c => c.ColumnId);
        });
    }
}
