using Microsoft.EntityFrameworkCore;
using SimpleMooc.Domain.Context.Courses.Entities;
using SimpleMooc.Domain.Context.Users.Entities;
using SimpleMooc.Infra.Maps;

namespace SimpleMooc.Infra.DataContext
{
    public class SimpleMoocDataContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                @"Host=localhost;Port=5434;Username=postgres;Password=postgres;Database=simplemooc;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ProfileMap());
            modelBuilder.ApplyConfiguration(new RefreshTokensMap());
            modelBuilder.ApplyConfiguration(new EnrollmentMap());
            modelBuilder.ApplyConfiguration(new LessonMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}