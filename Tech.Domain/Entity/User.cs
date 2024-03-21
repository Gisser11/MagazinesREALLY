using Microsoft.AspNetCore.Identity;
using Tech.Domain.Interfaces;

namespace Tech.Domain.Entity;

public class User : IdentityUser<long>
{
    public string PROVERKAJIEEEEST { get; set; }
    
    public ICollection<Author> Authors { get; set; }
    
    public ICollection<Article> Articles { get; set; }
}

