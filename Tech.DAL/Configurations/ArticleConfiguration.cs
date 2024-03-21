using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech.Domain.Entity;

namespace Tech.DAL.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        /*builder.HasData(new List<Article>()
        {
            new Article()
            {
                Id = 1,
                UserId = 1,
                Name = "Name1"
            },
            new Article()
            {
                Id = 2,
                UserId = 1,
                Name = "Name2"
            }
        });*/
    }
}