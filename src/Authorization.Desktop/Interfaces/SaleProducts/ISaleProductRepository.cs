using Authorization.Desktop.Entities.SaleProduct;
using Authorization.Desktop.ViewModels.SaleProducts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorization.Desktop.Interfaces.SaleProducts;

public interface ISaleProductRepository
{
    public Task<IList<SaleProductViewModel>> GetAllSaleProductsAsync(long tabId);
    public Task<int> CreateSaleProductAsync(SaleProduct saleProduct);
}
