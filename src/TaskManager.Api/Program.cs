using Microsoft.EntityFrameworkCore;
using Serilog;
using StackExchange.Redis;
using TaskManager.Api.Services;
using TaskManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//redis db
builder.Services.AddSingleton<IConnectionMultiplexer>(s =>
{
    var connectionString = builder.Configuration.GetConnectionString("redis");
    return ConnectionMultiplexer.Connect(connectionString);
});
builder.Services.AddScoped<IDatabase>(s =>
{
    var db = s.GetService<IConnectionMultiplexer>()?.GetDatabase();
    if (db == null) throw new NullReferenceException("Unable to connect to redis. No database instance found.");
    return db;
});

//mysql db
builder.Services.AddDbContext<TaskDbContext>(dbContextOptions =>
{
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
    var connectionString = builder.Configuration.GetConnectionString("mysql");
    dbContextOptions
        .UseMySql(connectionString, serverVersion)
         .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});
builder.Services.AddTransient<ITaskSearchService,TaskSearchService>();

//serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

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

app.Run();
