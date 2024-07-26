using Unisystem.ClassroomAccount.DataContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddClassroomContext(builder.Configuration.GetConnectionString("DefaultConnection"));
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
