using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleMooc.Domain.Context.Courses.Services;
using SimpleMooc.Domain.Context.Users.Services;
using SimpleMooc.Infra.Services;
using SimpleMooc.Shared.Config;
using SimpleMooc.Shared.Services;

namespace SimpleMooc.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDependencyServices(this IServiceCollection service,
            IConfiguration configuration)
        {
            var cloudinaryConfig = configuration.GetSection("Cloudinary").Get<CloudinaryConfig>();

            service.AddSingleton<CloudinaryConfig, CloudinaryConfig>(_ => cloudinaryConfig);

            service.AddTransient<ICourseService, CourseService>();
            service.AddTransient<IUploadService, UploadService>();
            service.AddTransient<ITokenService, TokenService>();
            service.AddTransient<IProfileService, ProfileService>();
            service.AddTransient<IUploadService, UploadService>();

            return service;
        }
    }
}