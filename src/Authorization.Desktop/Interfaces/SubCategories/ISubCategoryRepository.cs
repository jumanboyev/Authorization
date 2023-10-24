using Authorization.Desktop.Entities.Subcategories;
using Authorization.Desktop.ViewModels.SubCategories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorization.Desktop.Interfaces.SubCategories;

public interface ISubCategoryRepository:IRepository<SubCategory,SubCategoryViewModel>
{
    public Task<IList<SubCategoryViewModel>> GetAllByIdAsync(long CategoryId);

    public Task<bool> GetByIdSubCategoryName(long categoryId, string subCategoryName);
}
