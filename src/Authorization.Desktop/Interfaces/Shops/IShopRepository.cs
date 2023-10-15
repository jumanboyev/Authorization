using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.ViewModels.Shops;

namespace Authorization.Desktop.Interfaces.Shops;

public interface IShopRepository : IRepository<Shop,ShopViewModel>
{
}
