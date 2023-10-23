namespace Authorization.Desktop.Entities.Shops;

public class Shop:Auditable
{
    public long UserId { get; set; }
    public string Name { get; set; } = string.Empty;
}
