namespace Authorization.Desktop.Entities.Shops;

public class Shop:Auditable
{
    public long CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
