using Unisystem.BuildingAccount.DataContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBuildingContext(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddControllers();

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

app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(async () =>
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<BuildingContext>();
    await dbContext.InitializeDatabaseAsync();
});

app.Run();
