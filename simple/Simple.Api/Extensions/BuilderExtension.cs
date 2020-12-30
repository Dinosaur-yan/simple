using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simple.Api.Extensions.AppBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Api.Extensions
{
    public static class BuilderExtension
    {
        public static void AddServiceProvider(this IServiceCollection services,
            IConfiguration configuration)
        {
            IServiceBuilder serviceBuilder = new ServiceBuilder(services, configuration);
            serviceBuilder.AddControllers();
            serviceBuilder.AddSwaggerGen();
        }

        public static void AddConfigurationProvider(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            AppBuilder.IConfigurationBuilder configurationBuilder = new AppBuilder.ConfigurationBuilder(app, env);
            configurationBuilder.UseDefaultConfiguration();
            configurationBuilder.UseEndpointsAndAuth();
            configurationBuilder.UseSwagger();
        }
    }
}
