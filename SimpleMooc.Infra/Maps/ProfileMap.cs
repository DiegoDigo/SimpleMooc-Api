using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMooc.Domain.Context.Users.Entities;

namespace SimpleMooc.Infra.Maps
{
    public class ProfileMap : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profile");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.LastName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.FullName).HasColumnType("varchar(155)").IsRequired();
            builder.Property(x => x.UrlImage).HasColumnType("varchar(255)");
            builder.HasOne(x => x.User);
            builder.Property(x => x.CreateAt).HasColumnType("timestamp").ValueGeneratedOnAdd();
            builder.Property(x => x.UpdateAt).HasColumnType("timestamp").ValueGeneratedOnUpdate();
        }
    }
}