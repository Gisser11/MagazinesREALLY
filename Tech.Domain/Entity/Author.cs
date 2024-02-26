using Microsoft.VisualBasic.CompilerServices;
using Tech.Domain.Interfaces;

namespace Tech.Domain.Entity;

public class Author : IEntityId<long>, IAuthor
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public User User { get; set; }
    
    public List<Article> Articles { get; set; }
    
    public string WorkPlace { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string WorkPosition { get; set; }
    public string WorkGrade { get; set; }
    public int ElibraryId { get; set; }
}