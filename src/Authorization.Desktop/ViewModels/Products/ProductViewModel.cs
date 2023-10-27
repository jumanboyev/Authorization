using System;

namespace Authorization.Desktop.ViewModels.Products;

public class ProductViewModel
{
    public long Id { get; set; }
    public long SubCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public long BarCode { get; set; }
    public long Quantity { get; set; }
    public long SoldPrice { get; set; }
    public long Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set;}
}
