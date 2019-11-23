﻿using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultJsonSettings(this IServiceCollection services)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateCamelcaseResolver()
            };
            return services;
        }

        public static IServiceCollection AddJsonAppSettings(this IServiceCollection services)
        {
            return services.AddSingleton<IConfiguration>(_ => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());
        }

        public static IServiceCollection AddPomeloCors(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetSection("ClientOrigins").Get<string[]>();
            return services.AddCors(o =>
            {
                var policy = new CorsPolicyBuilder(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .Build();
                o.AddPolicy("PomeloCors", policy);
            });
        }

        public static IServiceCollection AddRepositoryMediator(this IServiceCollection services)
        {
            return services.AddScoped<IRepositoryMediator, RepositoryMediator>();
        }

        public static IServiceCollection AddPomeloSwagger(this IServiceCollection services, string title, string version = "1")
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{version}", new OpenApiInfo {Title = title, Version = $"v{version}"});
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });
        }

        public static IServiceCollection AddPomeloJwtAuthentication(this IServiceCollection services, IJwtSettings jwtSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = jwtSettings.SecurityKey
                };
            });

            return services;
        }
    }
}