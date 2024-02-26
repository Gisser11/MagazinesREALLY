namespace Tech.Domain.Interfaces;

public interface IAuthor
{
    public string WorkPlace { get; set; }
    
    public string Country { get; set; }
    
    public string City { get; set; }
    
    public string WorkPosition { get; set; }
    
    public string WorkGrade { get; set; }
    
    public int ElibraryId { get; set; }
}