using FitVision.Api.Services;
using FitVision.Application;
using FitVision.Application.Interfaces;
using FitVision.Application.Mapping;
using FitVision.Infrastructure;
using FitVision.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// AutoMapper
builder.Services.AddAutoMapper(cfg => { cfg.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"]; }, typeof(MappingProfile));

// Dependency Injection (Application, Infrastructure)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

// Services
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

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

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.Run();