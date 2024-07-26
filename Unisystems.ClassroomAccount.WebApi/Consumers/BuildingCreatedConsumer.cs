using MassTransit;
using Unisystems.ClassroomAccount.WebApi.BuildingService;
using Unisystems.ClassroomAccount.WebApi.Mapper;
using Unisystems.RabbitMq.BuildingMessageContracts;

namespace Unisystems.RabbitMq.Consumers;

internal class BuildingCreatedConsumer : IConsumer<BuildingCreated>
{
    private readonly ILogger<BuildingCreatedConsumer> _logger;
    private readonly IBuildingService _buildingService;

    public BuildingCreatedConsumer(ILogger<BuildingCreatedConsumer> logger, IBuildingService buildingService)
    {
        _logger = logger;
        _buildingService = buildingService;
    }

    public async Task Consume(ConsumeContext<BuildingCreated> context)
    {
        await _buildingService.CreateAsync(context.Message.ToBuildingEntity());
        await _buildingService.SaveChangesAsync();

        _logger.LogInformation($"Building created: {context.Message.BuildingId}");
    }
}
