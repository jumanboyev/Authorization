using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.ViewModels.Categories;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorization.Desktop.Interfaces.Categories;

public interface ICategoryRepository:IRepository<Category,CategoryViewModel>
{
    public Task<IList<CategoryViewModel>> GetAllByIdAsync(long shopId);
}
