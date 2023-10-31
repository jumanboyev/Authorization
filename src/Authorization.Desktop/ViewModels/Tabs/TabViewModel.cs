using System;

namespace Authorization.Desktop.ViewModels.Tabs;

public class TabViewModel
{
    public long Id { get; set; }
    public long CashboxId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
