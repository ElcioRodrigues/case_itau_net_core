using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace CaseItau.API.Configuration
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CaseItau API",
                    Version = "v1",
                    Description = "Case itau engenharia",
                    Contact = new OpenApiContact
                    {
                        Name = "",
                        Email = "",
                    },
                    License = new OpenApiLicense
                    {
                        Name = ""
                    }
                });
                swagger.ResolveConflictingActions(x => x.First());
            });
            return services;
        }
    }
}
