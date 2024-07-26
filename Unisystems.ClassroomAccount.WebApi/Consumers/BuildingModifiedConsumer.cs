using MassTransit;
using Unisystems.ClassroomAccount.WebApi.BuildingService;
using Unisystems.ClassroomAccount.WebApi.Mapper;
using Unisystems.RabbitMq.BuildingMessageContracts;

namespace Unisystems.RabbitMq.Consumers;

public class BuildingModifiedConsumer : IConsumer<BuildingModified>
{
    private readonly ILogger<BuildingModifiedConsumer> _logger;
    private readonly IBuildingService _buildingService;

    public BuildingModifiedConsumer(ILogger<BuildingModifiedConsumer> logger, IBuildingService buildingService)
    {
        _logger = logger;
        _buildingService = buildingService;
    }

    public async Task Consume(ConsumeContext<BuildingModified> context)
    {
        _buildingService.Update(context.Message.ToBuildingEntity());
        await _buildingService.SaveChangesAsync();
        _logger.LogInformation($"Building modified: {context.Message.BuildingId}");
    }
}
