using System;

namespace Authorization.Desktop.ViewModels.Shops;

public class ShopViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdatedAt { get; set; }

}
