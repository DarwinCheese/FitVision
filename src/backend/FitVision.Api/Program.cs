using FitVision.Application.Commands.CreateMeal;
using FitVision.Application.Mapping;
using FitVision.Domain.Interfaces;
using FitVision.Infrastructure.Middleware;
using FitVision.Infrastructure.Persistence;
using FitVision.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// MediatR (scan applicatie assembly)
builder.Services.AddMediatR(cfg => { 
    cfg.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"]; ;
    cfg.RegisterServicesFromAssemblies(typeof(CreateMealHandler).Assembly); 
    }
);

// AutoMapper
builder.Services.AddAutoMapper(cfg => { cfg.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"]; }, typeof(MappingProfile));

// Infrastructure - register repos
builder.Services.AddScoped<IMealRepository, MealRepository>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.Run();