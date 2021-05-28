using Microsoft.Extensions.DependencyInjection;
using SimpleMooc.Domain.Context.Courses.Repositories;
using SimpleMooc.Domain.Context.Users.Repositories;
using SimpleMooc.Infra.DataContext;
using SimpleMooc.Infra.Repositories;
using SimpleMooc.Shared.Repositories;

namespace SimpleMooc.Api.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddDependencyRepository(this IServiceCollection service)
        {
            service.AddScoped<SimpleMoocDataContext, SimpleMoocDataContext>();
            service.AddTransient<ICourseRepository, CourseRepository>();
            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            service.AddTransient<IProfileRepository, ProfileRepository>();
            service.AddTransient<IEnrollmentRepository, EnrollmentRepository>();
            service.AddTransient<ILessonRepository, LessonRepository>();
            service.AddTransient<IUnitOfWork, UnitOfWork>();

            return service;
        }
    }
}