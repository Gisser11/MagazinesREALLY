using Tech.Domain.Interfaces;

namespace Tech.Domain.Entity;

public class Role : IEntityId<long>
{
    public long Id { get; set; }

    public string Name { get; set; }

    public List<User> Users { get; set; }

    
}