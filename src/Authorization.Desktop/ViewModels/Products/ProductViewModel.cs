using System;

namespace Authorization.Desktop.ViewModels.Products;

public class ProductViewModel
{
    public long Id { get; set; }
    public long SubCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public long Barcode { get; set; }
    public long Quantity { get; set; }
    public float SoldPrice { get; set; }
    public float Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set;}
}
