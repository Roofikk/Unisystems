using MassTransit;
using Unisystems.BuildingAccount.DataContext;
using Unisystems.ClassroomAccount.WebApi.Services.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBuildingContext(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddMassTransit(x =>
{
    if (builder.Configuration["RabbitMq:Host"] == null ||
        builder.Configuration["RabbitMq:UserName"] == null ||
        builder.Configuration["RabbitMq:Password"] == null)
    {
        throw new Exception("RabbitMq configuration is not set. Need to set RabbitMq:Host, RabbitMq:UserName, RabbitMq:Password in appsettings.json");
    }

    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", h =>
        {
            h.Username(builder.Configuration["RabbitMq:UserName"]!);
            h.Password(builder.Configuration["RabbitMq:Password"]!);
        });

        cfg.ConfigureEndpoints(context);
        cfg.UseRawJsonSerializer();
    });
});
builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularFront",
        corsBuilder => corsBuilder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularFront");
app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(async () =>
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<BuildingContext>();
    await dbContext.InitializeDatabaseAsync();
});

app.Run();
