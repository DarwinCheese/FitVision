using FitVision.Infrastructure.InMemory;
using FitVision.Domain.Interfaces;
using FitVision.Application.Mapping;
using FitVision.Application.Commands.CreateMeal;
using MediatR;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// MediatR (scan applicatie assembly)
builder.Services.AddMediatR(cfg => { 
    cfg.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"]; ;
    cfg.RegisterServicesFromAssemblies(typeof(CreateMealHandler).Assembly); 
    }
);

// AutoMapper
builder.Services.AddAutoMapper(cfg => { cfg.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"]; }, typeof(MappingProfile));

// Infrastructure - register in-memory repo
builder.Services.AddSingleton<IMealRepository, InMemoryMealRepository>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();