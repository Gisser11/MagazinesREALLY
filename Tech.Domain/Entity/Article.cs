using Tech.Domain.Interfaces;

namespace Tech.Domain.Entity;

public class Article
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public string? RuClassifierArticle { get; set; }

    public string? DigitalObjId { get; set; }

    public string? Annotation { get; set; }

    public int? TextId { get; set; }

    public string[]? KeyWords { get; set; }
    
    public long UserId { get; set; }
    
    public User User { get; set; }
 
    
}