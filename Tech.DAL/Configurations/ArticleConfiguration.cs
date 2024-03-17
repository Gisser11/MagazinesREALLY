using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech.Domain.Entity;

namespace Tech.DAL.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        /*Guid guid = new Guid("864419b1-0479-49be-86ab-67bb0a6d9ec2");
        builder.HasData(new List<Article>()
        {
            new Article()
            {
                Id = new Guid("864419b1-0479-49be-86ab-67bb0a6d9ec5"),
                UserId = guid,
                Name = "Name1"
            },
            new Article()
            {
                Id = new Guid("864419b1-0479-49be-86ab-67bb0a6d9ec6"),
                UserId = guid,
                Name = "Name2"
            }
        });*/
    }
}