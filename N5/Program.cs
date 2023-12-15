using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using N5.Infrastructure;
using N5.Infrastructure.Repositories;
using N5.Infrastructure.UnitOfWork;
using N5.Interfaces;
using N5.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//ElasticSearch y Kafka
builder.Services.AddScoped<IElasticSearchService, ElasticSearchService>();
builder.Services.AddScoped<IKafkaProducerService, KafkaProducerService>();

//Agregando Mediator y Persistencia en DB
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Permissions")));

//Repository + Unit of Work
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
