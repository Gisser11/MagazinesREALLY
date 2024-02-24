using Tech.Domain.Interfaces;

namespace Tech.Domain.Entity;

public class Article: IEntityId<long>, IAuditable
{
    public long Id { get; set; }

    public string Name { get; set; }


    public string RuClassifierArticle { get; set; }

    public string DigitalObjId { get; set; }

    public string Annotation { get; set; }

    public int TextId { get; set; }

    public string[] KeyWords { get; set; }

    public Author Author { get; set; }

    public long AuthorId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public long CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public long? UpdatedBy { get; set; }
    
}