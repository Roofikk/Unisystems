using MassTransit;
using Unisystems.ClassroomAccount.DataContext;
using Unisystems.ClassroomAccount.WebApi.BuildingService;
using Unisystems.ClassroomAccount.WebApi.RoomTypeService;
using Unisystems.RabbitMq.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddClassroomContext(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
builder.Services.AddMassTransit(x =>
{
    if (builder.Configuration["RabbitMq:Host"] == null ||
        builder.Configuration["RabbitMq:UserName"] == null ||
        builder.Configuration["RabbitMq:Password"] == null)
    {
        throw new ArgumentException("RabbitMq configuration not found. Need RabbitMq:Host, RabbitMq:UserName, RabbitMq:Password in appsettings.json");
    }

    x.SetKebabCaseEndpointNameFormatter();

    x.AddConsumer<BuildingCreatedConsumer>();
    x.AddConsumer<BuildingModifiedConsumer>();
    x.AddConsumer<BuildingDeletedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], h =>
        {
            h.Username(builder.Configuration["RabbitMq:UserName"]!);
            h.Password(builder.Configuration["RabbitMq:Password"]!);
        });

        cfg.ReceiveEndpoint("building-created-queue", e =>
        {
            e.ConfigureConsumer<BuildingCreatedConsumer>(context);
        });

        cfg.ReceiveEndpoint("building-modified-queue", e =>
        {
            e.ConfigureConsumer<BuildingModifiedConsumer>(context);
        });

        cfg.ReceiveEndpoint("building-deleted-queue", e =>
        {
            e.ConfigureConsumer<BuildingDeletedConsumer>(context);
        });

        cfg.UseRawJsonDeserializer();
    });
});
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(async () =>
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ClassroomContext>();

    await context.InitializeDatabaseAsync();
});

app.Run();
