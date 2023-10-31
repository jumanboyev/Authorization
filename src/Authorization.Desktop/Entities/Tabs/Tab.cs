namespace Authorization.Desktop.Entities.Tabs;

public class Tab:Auditable
{
    public long CashboxId { get; set; }
    public string Name { get; set; } = string.Empty;
}
