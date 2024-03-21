using Microsoft.AspNetCore.Identity;
using Tech.Domain.Interfaces;

namespace Tech.Domain.Entity;

public class User
{
    public Guid Id { get; set; }
    
    public string IdentityId { get; set; }
    
    public ICollection<Author> Authors { get; set; }
    
    public ICollection<Article> Articles { get; set; }
}

