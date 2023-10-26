namespace Authorization.Desktop.ViewModels.Products;

public class ProductAllToStorageViewModel
{
    public long Id { get; set; }
    public string Category { get; set; }= string.Empty;
    public string Subcategory { get; set; }= string.Empty;
    public string Name { get; set; } = string.Empty;
    public long BarCode { get; set; }
    public long Quantity { get; set; }
    public float SoldPrice { get; set; }
    public float Price { get; set; }
}
