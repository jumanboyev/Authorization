using Authorization.Desktop.Entities.Tabs;
using Authorization.Desktop.ViewModels.SubCategories;
using Authorization.Desktop.ViewModels.Tabs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorization.Desktop.Interfaces.Tabs;

public interface ITabRepository : IRepository<Tab, TabViewModel>
{
    public Task<IList<TabViewModel>> GetAllByIdAsync(long cashboxId);

    public Task<bool> GetByIdTabName(long cashboxId, string tabName);
}
