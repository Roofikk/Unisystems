using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Reflection;
using Unisystem.ClassroomAccount.DataContext.Entities;
using Unisystem.ClassroomAccount.DataContext.ForJson;

namespace Unisystem.ClassroomAccount.DataContext;

public static class ClassroomContextExtensions
{
    public static IServiceCollection AddClassroomContext(this IServiceCollection services, string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException(nameof(connectionString), "Connection string cannot be null");
        }

        return services.AddDbContext<ClassroomContext>(options =>
        {
            options.UseNpgsql(connectionString, npgOptions =>
            {
                npgOptions.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
                npgOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    null);
            });
        });
    }

    public static async Task InitializeDatabaseAsync(this ClassroomContext context)
    {
        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            await context.Database.MigrateAsync();
        }

        var existingRoomTypes = await context.RoomTypes
            .AsNoTracking()
            .ToListAsync();

        string assemblyPath = Assembly.GetExecutingAssembly().Location;
        string filePath = Path.Combine(Path.GetDirectoryName(assemblyPath)!, "neededRoomTypes.json");
        string jsonString = File.ReadAllText(filePath);

        var neededRoomTypes = JsonConvert.DeserializeObject<RoomDataCollection>(jsonString)
            ?? throw new Exception("Failed to deserialize needed room types from JSON");

        var missingRoomTypes = neededRoomTypes.RoomTypes
            .ExceptBy(existingRoomTypes.Select(x => x.KeyName), x => x.KeyName)
            .ToList();

        foreach (var roomType in missingRoomTypes)
        {
            await context.RoomTypes.AddAsync(new RoomType
            {
                KeyName = roomType.KeyName,
                DisplayName = roomType.DisplayName
            });
        }

        if (context.ChangeTracker.HasChanges())
        {
            await context.SaveChangesAsync();
        }
    }
}
