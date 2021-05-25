using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SimpleMooc.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddDependencySwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "SimpleMooc.Api", Version = "v1"});
            });

            return services;
        }
    }
}