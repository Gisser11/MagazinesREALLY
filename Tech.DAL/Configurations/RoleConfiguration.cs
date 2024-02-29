using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech.Domain.Entity;

namespace Tech.DAL.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.HasData(new List<Role>()
        {
            new Role()
            {
                Id = 1,
                Name = "User"
            },
            new Role()
            {
                Id = 2, 
                Name = "Admin"
            },
            new Role()
            {
            Id = 3, 
            Name = "Moderator"
        }
        });
    }
}