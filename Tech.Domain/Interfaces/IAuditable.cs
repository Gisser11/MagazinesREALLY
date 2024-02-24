namespace Tech.Domain.Interfaces;

/// <summary>
/// Каждый раз, когда мы дергаем нашу сущность, будут изменяться даты
/// добавления или времени. Например, при модификации какого-либо из
/// полей сущности User, UpdatedBy перезапишет свое значение
/// </summary>
public interface IAuditable
{
    public DateTime CreatedAt { get; set; }

    public long CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
    
    public long? UpdatedBy { get; set; }
}
