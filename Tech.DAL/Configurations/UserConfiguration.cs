using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech.Domain.Entity;

namespace Tech.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.HasOne(u => u.Author)
            .WithOne(a => a.User)
            .HasForeignKey<Author>(a => a.UserId)
            .HasPrincipalKey<User>(u => u.Id);
    }
}