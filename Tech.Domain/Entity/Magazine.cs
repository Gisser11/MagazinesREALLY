using Tech.Domain.Interfaces;

namespace Tech.Domain.Entity;

public class Magazine :IEntityId<long>
{
    public long Id { get; set; }

    public string Name { get; set; }

    public int Year { get; set; }
    
    public string File { get; set; }
}
