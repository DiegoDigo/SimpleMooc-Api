using System;
using System.IO;
using System.Reflection;
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
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SimpleMooc.Api",
                    Version = "v1",
                    Description = "Simple api de cursos.",
                    Contact = new OpenApiContact
                    {
                        Name = "Diego Domingos Delmiro",
                        Email = "di3g0d0ming05@gmail.com",
                        Url = new Uri("https://github.com/DiegoDigo"),
                    },
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}