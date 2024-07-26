using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Unisystems.BuildingAccount.DataContext;

public static class BuildingContextExtensions
{
    public static IServiceCollection AddBuildingContext(this IServiceCollection services, string? connectionString)
    {
        if (connectionString is null)
        {
            throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null");
        }

        return services.AddDbContext<BuildingContext>(options =>
        {
            options.UseNpgsql(connectionString, npgOptions =>
            {
                npgOptions.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                npgOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    null);
            });
        });
    }

    public static async Task InitializeDatabaseAsync(this BuildingContext context)
    {
        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            await context.Database.MigrateAsync();
        }
    }
}
