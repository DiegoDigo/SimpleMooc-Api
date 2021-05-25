using Microsoft.Extensions.DependencyInjection;

namespace SimpleMooc.Api.Extensions
{
    public static class ApiExploreExtensions
    {
        public static IServiceCollection AddDependencyApiExplore(this IServiceCollection services)
        {
            services.AddApiVersioning();
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            return services;
        }
    }
}