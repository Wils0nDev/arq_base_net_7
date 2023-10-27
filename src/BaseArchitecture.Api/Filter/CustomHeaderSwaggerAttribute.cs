using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BaseArchitecture.Api.Filter
{
    public class CustomHeaderSwaggerAttribute : IOperationFilter
    {

        private List<string> ExcludePath = new List<string>() {
            "api/Test/SerializeObject",
            "api/Test/GetUserToken",
            "api/Test/Prueba",
        };

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {


            var isFilter = ExcludePath.Any(t => context.ApiDescription.RelativePath.ToLower().Contains(t.ToLower().Trim()));
            if (!isFilter)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Code",
                    In = ParameterLocation.Header,
                    Required = false,
                    //Schema = new OpenApiSchema
                    //{
                    //    Type = "string"
                    //}
                });

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Header",
                    In = ParameterLocation.Header,
                    Required = false,
                    Description = "Token encriptado del usuario.",
                });
            }
        }
    }

}
