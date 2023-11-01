namespace Authorization.Desktop.ViewModels.SaleProducts;

public class SaleProductViewModel
{
    public long Id { get; set; }
    public long TabId { get; set; }
    public string Name { get; set; } = string.Empty;
    public long BarCode { get; set; }
    public long Quantity { get; set; }
    public long SoldPrice { get; set; }
    public long Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Subcategory { get; set; } = string.Empty;
}
