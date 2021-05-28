using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMooc.Domain.Context.Courses.Entities;

namespace SimpleMooc.Infra.Maps
{
    public class LessonMap : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lesson");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Course);
            builder.Property(x => x.Number).HasColumnType("smallint").IsRequired();
            builder.Property(x => x.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("varchar(255)");
            builder.Property(x => x.UrlVideos).HasColumnType("varchar(255)").IsRequired();
            builder.Property(x => x.ReleaseDate).HasColumnType("timestamp").IsRequired();
            builder.Property(x => x.CreateAt).HasColumnType("timestamp").ValueGeneratedOnAdd();
            builder.Property(x => x.UpdateAt).HasColumnType("timestamp").ValueGeneratedOnUpdate();
        }
    }
}