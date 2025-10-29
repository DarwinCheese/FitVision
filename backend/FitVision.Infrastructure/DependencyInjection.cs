using FitVision.Application.Interfaces;
using FitVision.Infrastructure.Persistence;
using FitVision.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVision.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Repositories
            services.AddScoped<IMealRepository, MealRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Security
            services.AddScoped<IPasswordHasher, Security.BcryptPasswordHasher>();
            services.AddScoped<IJwtTokenService, Security.JwtTokenService>();

            // Authentication and Authorization
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
                    };
                });

            services.AddAuthorization();

            return services;
        }
    }
}
