using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace ServiceManagement.API.Extensions.Swagger
{
    public class SwaggerUploadFileFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!context.ApiDescription.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase)
                && !context.ApiDescription.HttpMethod.Equals("PUT", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            var apiDescription = context.ApiDescription;

            var parameters = context.ApiDescription.ParameterDescriptions.Where(n => n.Type == typeof(IFormFileCollection)
            || n.Type == typeof(IFormFile)).ToList();

            if (parameters == null || parameters.Count() <= 0)
            {
                return;
            }

            foreach (var fileParameter in parameters)
            {
                var parameter = operation.Parameters.FirstOrDefault(n => n.Name == fileParameter.Name);
                if (parameter == null) continue;
                operation.Parameters.Remove(parameter);
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = parameter.Name,
                    In = ParameterLocation.Header,
                    Description = parameter.Description,
                    Required = parameter.Required
                });
            }
        }
    }
}
