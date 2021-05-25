using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMooc.Domain.Context.Courses.Entities;

namespace SimpleMooc.Infra.Maps
{
    public class CourseMap : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Cursos");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Slug).IsRequired().HasColumnType("varchar(255)").IsRequired();
            builder.HasIndex(x => x.Slug).IsUnique();
            builder.Property(x => x.Description).HasColumnType("varchar(255)");
            builder.Property(x => x.StartDate).HasColumnType("timestamp").IsRequired();
            builder.Property(x => x.CreateAt).HasColumnType("timestamp").ValueGeneratedOnAdd();
            builder.Property(x => x.UpdateAt).HasColumnType("timestamp").ValueGeneratedOnUpdate();
        }
    }
}