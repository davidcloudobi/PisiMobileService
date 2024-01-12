using PisiMobile.BusinessCore.Integration;
using PisiMobile.CoreObject.Models;
using Microsoft.EntityFrameworkCore;
using PisiMobile.WebAPI.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DbConnString");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Host.ConfigureLogging(logging =>
{
    logging.AddLog4Net(log4NetConfigFile: "log4net.config");
    logging.ClearProviders();
    logging.AddConsole();//for Logging on Console 
    logging.AddLog4Net();//for DB Query Logging
});

builder.Logging.AddLog4Net();

// Register services

ServiceExtensions.DependencyInjection(builder.Services);
ServiceExtensions.AppSettings(builder.Services, builder.Configuration);
ServiceExtensions.ConfigureAuthJWT(builder.Services, builder.Configuration);
ServiceExtensions.ConfigureCors(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.ConfigureExceptionHandler();

app.UseHttpsRedirection();

app.UseGlobalCustomMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
