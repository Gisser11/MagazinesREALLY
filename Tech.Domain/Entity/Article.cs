using Tech.Domain.Interfaces;

namespace Tech.Domain.Entity;

public class Article: IEntityId<Guid>, IAuditable
{
    public Guid Id { get; set; }

    public string Name { get; set; }


    public string RuClassifierArticle { get; set; }

    public string DigitalObjId { get; set; }

    public string Annotation { get; set; }

    public int TextId { get; set; }

    public string[] KeyWords { get; set; }

    public Author Author { get; set; }

    public Guid AuthorId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public long CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public long? UpdatedBy { get; set; }
    
}