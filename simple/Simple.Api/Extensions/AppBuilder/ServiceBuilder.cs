using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Simple.Api.Extensions.JwtAuth;
using Simple.Api.Extensions.Swagger;
using Simple.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Simple.Api.Extensions.AppBuilder
{
    public class ServiceBuilder : IServiceBuilder
    {
        private readonly IServiceCollection services;
        private readonly IConfiguration configuration;

        public ServiceBuilder(IServiceCollection services, IConfiguration configuration)
        {
            this.services = services;
            this.configuration = configuration;
        }

        public void AddAuthentication()
        {
            var tokenSection = configuration.GetSection("Security:Token");

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSection["Key"]));

            services.Configure<JwtOptions>(options =>
            {
                options.Issuer = tokenSection["Issuer"];
                options.Audience = tokenSection["Audience"];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = tokenSection["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = tokenSection["Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });
        }

        public void AddAutoMapper()
        {
            services.AddAutoMapper(typeof(Startup));
        }

        public void AddControllers()
        {
            services.AddMemoryCache();

            services.AddControllers(configure =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                configure.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddNewtonsoftJson(JsonOptions)
            .AddXmlSerializerFormatters();
        }

        private void JsonOptions(MvcNewtonsoftJsonOptions json)
        {
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
            json.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            json.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        public void AddDbContext()
        {
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseMySQL(configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public void AddSwaggerGen()
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Simple.Api", Version = "v1" });

                var xmls = new string[] { "Simple.Api.xml" };
                foreach (var xml in xmls)
                {
                    var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "Swagger", xml);
                    options.IncludeXmlComments(xmlPath, true);
                }

                options.OperationFilter<SwaggerUploadFileFilter>();

                var security = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference{ Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                };
                options.AddSecurityRequirement(security);
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Format: {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });
            });
        }

        public void RegisterService()
        {
            services.AddScoped<IJwtFactory, JwtFactory>();

            var assemblyRepository = Assembly.Load("Simple.Domain");
            RegisterService(assemblyRepository.GetTypes().Where(x => x.IsClass && x.Name.EndsWith("Repository")));

            var assemblyService = Assembly.Load("Simple.Application");
            RegisterService(assemblyService.GetTypes().Where(x => x.IsClass && x.Name.EndsWith("Service")));
        }

        private void RegisterService(IEnumerable<Type> types)
        {
            if (types == null || !types.Any()) return;

            foreach (var type in types)
            {
                var itype = type.GetTypeInfo().GetInterfaces()?.FirstOrDefault(x => x.Name.ToLower().IndexOf(type.Name.ToLower()) > -1);
                if (itype != null)
                    services.AddScoped(itype, type);
            }
        }
    }
}
