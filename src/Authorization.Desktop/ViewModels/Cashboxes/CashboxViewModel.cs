using System;

namespace Authorization.Desktop.ViewModels.Cashboxes;

public class CashboxViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
