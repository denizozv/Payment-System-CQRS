namespace Base;

public class BaseEntity
{
    public long Id { get; set; }
    public string InsertedUser { get; set; }
    public DateTime InsertedDate { get; set; }
    public string? UpdatedUser { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsActive { get; set; }
}