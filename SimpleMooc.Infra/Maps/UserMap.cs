using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMooc.Domain.Context.Users.Entities;

namespace SimpleMooc.Infra.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).HasColumnType("varchar(255)").IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Password).HasColumnType("varchar(60)").IsRequired();
            builder.Property(x => x.Active).HasColumnType("boolean").IsRequired();
            builder.Property(x => x.Role).HasColumnType("smallint").IsRequired().HasConversion<int>();
            builder.Property(x => x.CreateAt).HasColumnType("timestamp").ValueGeneratedOnAdd();
            builder.Property(x => x.UpdateAt).HasColumnType("timestamp").ValueGeneratedOnUpdate();
            
        }
    }
}