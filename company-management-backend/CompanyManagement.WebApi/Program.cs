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
using Serilog.Exceptions;
using Service;

string outputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss} TRACE={TraceId} {Level:u3}] - {Message}{NewLine}{Exception}";

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
        .WriteTo.Console(outputTemplate: outputTemplate)
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails();
});

// Add services to the container.
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("DevelopmentPolicy", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddDbContext<ManagementDbContext>();
builder.Services.AddSingleton<ApiKeyAuthorizationFilter>();
builder.Services.AddSingleton<IApiKeyValidator, ApiKeyValidator>();

builder.Services.AddScoped<IIndustryRepository, IndustryRepository>();
builder.Services.AddScoped<IIndustryService, IndustryService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MediatorEntryPointHelper).Assembly));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();
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