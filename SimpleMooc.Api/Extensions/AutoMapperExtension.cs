using System;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleMooc.Api.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddDependencyAutoMapper(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("SimpleMooc.Domain");
            services.AddAutoMapper(assembly);
            return services;
        }
    }
}