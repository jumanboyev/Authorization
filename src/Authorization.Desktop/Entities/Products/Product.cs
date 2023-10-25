namespace Authorization.Desktop.Entities.Products;

public class Product : Auditable
{
    public long SubCategoryId { get; set; }
    public string Name { get; set; }=string.Empty;
    public long BarCode { get; set; }
    public long Quantity { get; set; }
    public float SoldPrice { get; set; }
    public float Price { get; set; }

}
