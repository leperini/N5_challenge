using N5_challenge.Extensions;
using Persistence;
using Infraestructure;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatRExtensions();
builder.Services.AddPersistenceInfrastructure(config, builder.Environment.IsEnvironment("IntegrationTests"));
builder.Services.AddLoggerExtensions(config);
builder.Services.AddInfraestructure(config);

builder.Services.AddRestApiServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<N5DbContext>();
    dbcontext.Database.EnsureCreated();
    //dbcontext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseErrorHandlingMiddleware();
app.UseApiLoggerMiddleware();
app.UseKafkaIntegrationMiddleware();
app.Run();
