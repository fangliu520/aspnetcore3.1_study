using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPICore3.Models
{
    public class AddTokenHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }
            var attrs = context.ApiDescription.CustomAttributes();
            foreach (var attr in attrs)
            {
                // 如果 Attribute 是我们自定义的验证过滤器
                if (attr.GetType() == typeof(CheckTokenFilter))
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = "Token", 
                        In = ParameterLocation.Header,
                        Description = "access_token",
                        Required = true
                    });
                }
            }
        }
    }
}
