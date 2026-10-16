using Microsoft.EntityFrameworkCore;
using The_Town_Cleaner.Server.Models.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMagicOnion();
builder.Services.AddMvcCore().AddApiExplorer();

builder.Services.AddDbContext<GameDbContext>(options => {
#if DEBUG
    var connectionString = builder.Configuration.GetConnectionString("Default");
#else
    var connectionString = builder.Configuration.GetConnectionString("Production");
#endif

    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions => {
            mySqlOptions.EnableStringComparisonTranslations();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapMagicOnionService();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();