using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech.Domain.Entity;

namespace Tech.DAL.Configurations;

public class AuthorCongifuration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.Id);


        builder.Property(x => x.Country)
            .IsRequired().HasMaxLength(30);
        

        builder.Property(x => x.WorkPosition)
            .IsRequired();

        Guid guid = new Guid("864419b1-0479-49be-86ab-67bb0a6d9ec2");
        
        /*builder.HasData(new List<Author>()
        {
            new Author()
            {
                Id = new Guid("864419b1-0479-49be-86ab-67bb0a6d9ec4"),
                UserId = guid,
                Country = "cheb",
                WorkPosition = "google"
            },
            new Author()
            {
                Id = new Guid("864419b1-0479-49be-86ab-67bb0a6d9ec5"),
                UserId = guid,
                Country = "kazan",
                WorkPosition = "yandex"
            }
        });*/


    }
}