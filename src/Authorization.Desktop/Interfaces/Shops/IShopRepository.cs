using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.ViewModels.Shops;
using System.Threading.Tasks;

namespace Authorization.Desktop.Interfaces.Shops;

public interface IShopRepository : IRepository<Shop,ShopViewModel>
{
    public Task<bool> GetByIdShopNameAsync(string shopName);
}
