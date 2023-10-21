using System;

namespace Authorization.Desktop.ViewModels.Shops;

public class ShopViewModel
{
    public long Id { get; set; }
    public long CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdatedAt { get; set; }

}
