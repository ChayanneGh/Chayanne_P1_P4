using Chayanne_P1_P4.Service;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<AutoresService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();
//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

using (var scope = app.Services.CreateScope()) //inicializa DbSqlite_Ds y crea la tabla Numeros si no existe
{
    var svc = scope.ServiceProvider.GetRequiredService<AutoresService>();
    await svc.InitializeAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
