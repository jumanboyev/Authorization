namespace Authorization.Desktop.Entities.Subcategories;

public class SubCategory:Auditable
{
    public long CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
}
