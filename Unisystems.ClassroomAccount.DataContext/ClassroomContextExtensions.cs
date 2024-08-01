using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Reflection;
using Unisystems.ClassroomAccount.DataContext.Entities;
using Unisystems.ClassroomAccount.DataContext.ForJson;

namespace Unisystems.ClassroomAccount.DataContext;

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
                npgOptions.CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds);
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

        // Инициализация типов помещений
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

        // Инициализация таблицы типов данных
        var existingColumnTypes = await context.ColumnTypes
            .AsNoTracking()
            .ToListAsync();

        if (!existingColumnTypes.Any(x => x.ColumnTypeId == nameof(IntColumnValue)))
        {
            await context.ColumnTypes.AddAsync(new ColumnType
            {
                ColumnTypeId = nameof(IntColumnValue),
                DisplayName = "Целое число"
            });
        }

        if (!existingColumnTypes.Any(x => x.ColumnTypeId == nameof(DoubleColumnValue)))
        {
            await context.ColumnTypes.AddAsync(new ColumnType
            {
                ColumnTypeId = nameof(DoubleColumnValue),
                DisplayName = "Дробное число"
            });
        }

        if (!existingColumnTypes.Any(x => x.ColumnTypeId == nameof(InputColumnValue)))
        {
            await context.ColumnTypes.AddAsync(new ColumnType
            {
                ColumnTypeId = nameof(InputColumnValue),
                DisplayName = "Однострочный текст"
            });
        }

        if (!existingColumnTypes.Any(x => x.ColumnTypeId == nameof(TextAreaColumnValue)))
        {
            await context.ColumnTypes.AddAsync(new ColumnType
            {
                ColumnTypeId = nameof(TextAreaColumnValue),
                DisplayName = "Многострочный текст"
            });
        }

        if (context.ChangeTracker.HasChanges())
        {
            await context.SaveChangesAsync();
        }
    }
}
