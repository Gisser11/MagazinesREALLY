using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech.Domain.Entity;

namespace Tech.DAL.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasMany<Article>(x => x.Articles)
            .WithOne(x => x.Author)
            .HasForeignKey(x => x.AuthorId)
            .HasPrincipalKey(x => x.Id);
        
        builder.HasMany<Article>(x => x.Articles)
            .WithOne(x => x.Author)
            .HasForeignKey(x => x.AuthorId)
            .HasPrincipalKey(x => x.Id);


        builder.HasData(new List<Author>()
        {
            new Author()
            {
                Id = 1,
                UserId = 1,
                WorkGrade = "Middle",
                City = "Cheboksary",
                Country = "Ru"
            }
        });
    }
}