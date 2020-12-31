using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simple.Api.Extensions.AppBuilder;
using System.Text;

namespace Simple.Api.Extensions
{
    public static class BuilderExtension
    {
        public static void AddServiceProvider(this IServiceCollection services,
            IConfiguration configuration)
        {
            IServiceBuilder serviceBuilder = new ServiceBuilder(services, configuration);
            serviceBuilder.AddAuthentication();
            serviceBuilder.AddAutoMapper();
            serviceBuilder.AddDbContext();
            serviceBuilder.AddControllers();
            serviceBuilder.AddSwaggerGen();
            serviceBuilder.RegisterService();
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
