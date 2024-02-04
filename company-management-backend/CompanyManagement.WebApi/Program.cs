using CompanyManagement.Repository.Context;
using CompanyManagement.Repository.Repository.Implementation;
using CompanyManagement.Repository.Repository.Interface;
using CompanyManagement.Service.Implementation;
using CompanyManagement.Service.Interface;
using CompanyManagement.Shared.Interface.Repository;
using CompanyManagement.WebApi.Filter;
using CompanyManagement.WebApi.Helpers;
using Microsoft.EntityFrameworkCore;
using Serilog;


var logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm} [{Level:u3}] - {Message}{NewLine}{Exception}")
                .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("DevelopmentPolicy", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddDbContext<ManagementDbContext>();
builder.Services.AddSingleton< ApiKeyAuthorizationFilter>();
builder.Services.AddSingleton<IApiKeyValidator, ApiKeyValidator>();

builder.Services.AddScoped<IIndustryRepository, IndustryRepository>();
builder.Services.AddScoped<IIndustryService, IndustryService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("DevelopmentPolicy");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
await ApplyMigration();
app.Run();

async Task ApplyMigration()
{
    using var scope = app.Services.CreateScope();
    var _db = scope.ServiceProvider.GetRequiredService<ManagementDbContext>();
    if (_db != null)
    {
        if (_db.Database.GetPendingMigrations().Any())
        {
            Log.Information("Running the pending migration...");
            await _db.Database.MigrateAsync();
        }
    }
}