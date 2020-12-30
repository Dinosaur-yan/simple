using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple.Api.Extensions.AppBuilder
{
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private readonly IApplicationBuilder app;
        private readonly IWebHostEnvironment env;

        public ConfigurationBuilder(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.app = app;
            this.env = env;
        }

        public void UseDefaultConfiguration()
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }

        public void UseEndpointsAndAuth()
        {
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void UseSwagger()
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple.Api v1");

                //http://localhost:****/{RoutePrefix}/index.html
                options.RoutePrefix = string.Empty;
            });
        }
    }
}
