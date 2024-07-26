using MassTransit;
using Unisystems.ClassroomAccount.WebApi.BuildingService;
using Unisystems.RabbitMq.BuildingMessageContracts;

namespace Unisystems.RabbitMq.Consumers;

public class BuildingDeletedConsumer : IConsumer<BuildingDeleted>
{
    private readonly ILogger<BuildingDeletedConsumer> _logger;
    private readonly IBuildingService _buildingService;

    public BuildingDeletedConsumer(ILogger<BuildingDeletedConsumer> logger, IBuildingService buildingService)
    {
        _logger = logger;
        _buildingService = buildingService;
    }

    public async Task Consume(ConsumeContext<BuildingDeleted> context)
    {
        await _buildingService.DeleteAsync(context.Message.BuildingId);
        await _buildingService.SaveChangesAsync();

        _logger.LogInformation($"Building deleted: {context.Message.BuildingId}");
    }
}
