using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ServiceManagement.API.Extensions.Swagger;
using System;
using System.Collections.Generic;
using System.IO;

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

        public void AddControllers()
        {
            services.AddMemoryCache();

            services.AddControllers(options =>
            {
            })
            .AddNewtonsoftJson()
            .AddXmlSerializerFormatters();
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
    }
}
