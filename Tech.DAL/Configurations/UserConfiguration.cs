using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech.Domain.Entity;

namespace Tech.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Login).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Password).IsRequired();
        
        builder.HasOne<Author>(e => e.Author)
            .WithOne(e => e.User)
            .HasForeignKey<Author>(e => e.UserId)
            .IsRequired();
        
        builder.HasData(new List<User>()
        {
            new User()
            {
                Id = 1,
                Login = "gisser0",
                Password = "faqopl",
                Author = new Author()
                {
                    UserId = 1,
                    Country = "Russia"
                },
                CreatedAt = DateTime.UtcNow, 
            }
        });

    }
}