using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMooc.Domain.Context.Users.Entities;

namespace SimpleMooc.Infra.Maps
{
    public class RefreshTokensMap : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("Refresh");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Token).IsRequired().HasColumnType("varchar(255)");
            builder.HasIndex(x => x.Token).IsUnique();
            builder.Property(x => x.Expires).IsRequired().HasColumnType("timestamp");
            builder.Property(x => x.CreateAt).HasColumnType("timestamp").ValueGeneratedOnAdd();
            builder.Property(x => x.UpdateAt).HasColumnType("timestamp").ValueGeneratedOnUpdate();
            builder.HasOne(x => x.User);
        }
    }
}