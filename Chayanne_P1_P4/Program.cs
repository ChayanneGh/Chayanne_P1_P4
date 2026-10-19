using Chayanne_P1_P4.Service;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<SqliteConnetionFactory>();
builder.Services.AddScoped<AutoresService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

// Configure the HTTP request pipeline.
using (var scope = app.Services.CreateScope()) // Inicializa SQLite y crea la tabla Autores si no existe
{
    var svc = scope.ServiceProvider.GetRequiredService<SqliteConnetionFactory>();
    await svc.InitializeAsync();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();