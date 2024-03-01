using Tech.Domain.Interfaces;

namespace Tech.Domain.Entity;

//test
public class User/*aaaaa*/: IEntityId<long>, IAuditable
{//teeest
    //test
    //test
    //test
    public long Id { get; set; }

    public string Login { get; set; }
    
    public string Password { get; set; }

    public List<Role> Roles { get; set; }

    public UserToken UserToken { get; set; }
    
    
    public DateTime CreatedAt { get; set; }
    
    public long CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public long? UpdatedBy { get; set; }
}