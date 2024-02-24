using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech.Domain.Entity;

namespace Tech.DAL.Configurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.RefreshToken).IsRequired();
        builder.Property(x => x.RefreshTokenExpireTime).IsRequired();

        builder.HasData(new List<UserToken>()
        {
            new UserToken()
            {
                Id = 1,
                RefreshToken = "sadfasdfcxvb",
                RefreshTokenExpireTime = DateTime.UtcNow.AddDays(7),
                UserId = 1
            }
        });
    }
}