using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimpleMooc.Domain.Core.PipelineBehavior;

namespace SimpleMooc.Api.Extensions
{
    public static class MediatrExtensions
    {
        public static IServiceCollection AddDependencyMediaR(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("SimpleMooc.Domain");
            services.AddMediatR(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerBehavior<,>));

            return services;
        }
    }
}