using Authorization.Desktop.Entities.Products;
using Authorization.Desktop.ViewModels.Categories;
using Authorization.Desktop.ViewModels.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorization.Desktop.Interfaces.Products;

public interface IProductRepository:IRepository<Product,ProductViewModel>
{
    public Task<bool> GetByIdProductNameAsync(long subCategoryId,string productName);
    public Task<IList<ProductViewModel>> GetAllByIdAsync(long subCategoryId);

    public Task<IList<ProductAllToStorageViewModel>> GetAllProductToStorage();

}
