using Authorization.Desktop.Entities.SaleProduct;
using Authorization.Desktop.ViewModels.SaleProducts;
using System.Collections.Generic;

namespace Authorization.Desktop.Security;

public class IdentitySingleton
{
    public static IdentitySingleton _identitySingleton;
    public IList<SaleProductViewModel> AddToCartList { get; set; } = new List<SaleProductViewModel>();
    private IdentitySingleton()
    {

    }
    public static IdentitySingleton GetInstance()
    {
        if (_identitySingleton == null)
        {
            _identitySingleton = new IdentitySingleton();
        }
        return _identitySingleton;
    }
}
