using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMooc.Domain.Context.Courses.Entities;

namespace SimpleMooc.Infra.Maps
{
    public class EnrollmentMap : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollment");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Course);
            builder.HasOne(x => x.User);
            builder.Property(x => x.Status).HasColumnType("smallint").IsRequired().HasConversion<int>();
            builder.Property(x => x.CreateAt).HasColumnType("timestamp").ValueGeneratedOnAdd();
            builder.Property(x => x.UpdateAt).HasColumnType("timestamp").ValueGeneratedOnUpdate();
        }
    }
}