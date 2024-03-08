using Microsoft.AspNetCore.Identity;
using Tech.Domain.Interfaces;

namespace Tech.Domain.Entity;

public class User : IdentityUser
{
    //public List<Article> Articles { get; set; }
    
    public Author Author { get; set; }
}