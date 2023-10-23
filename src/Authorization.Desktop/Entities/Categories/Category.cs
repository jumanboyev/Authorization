namespace Authorization.Desktop.Entities.Categories;

public class Category:Auditable
{
    public long ShopId { get; set; }
    public string Name { get; set; } = string.Empty;
}
