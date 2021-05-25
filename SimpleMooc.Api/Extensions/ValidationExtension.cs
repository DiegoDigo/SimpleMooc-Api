using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleMooc.Api.Extensions
{
    public static class ValidationExtension
    {
        public static IServiceCollection AddDependencyValidation(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("SimpleMooc.Domain");
            services.AddValidatorsFromAssembly(assembly);
            return services;
        }
    }
}