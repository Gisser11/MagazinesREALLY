using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech.Domain.Entity;

namespace Tech.DAL.Configurations;

public class AuthorCongifuration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Country).IsRequired().HasMaxLength(30);
        builder.Property(x => x.City).IsRequired();
        builder.Property(x => x.WorkPosition).IsRequired();
        
        builder.HasMany<Article>(x => x.Articles)
            .WithOne(x => x.Author)
            .HasForeignKey(x => x.AuthorId)
            .HasPrincipalKey(x => x.Id);
    }
}