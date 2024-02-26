using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech.Domain.Entity;

namespace Tech.DAL.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        builder.HasData(new List<Article>()
        {
            new Article()
            {
                Id = 1,
                AuthorId = 1,
                Name = "Название1",
                KeyWords = ["key1", "key2"],
            }
        });
    }
}