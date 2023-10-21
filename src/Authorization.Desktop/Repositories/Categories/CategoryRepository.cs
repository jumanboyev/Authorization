using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Interfaces.Categories;
using Authorization.Desktop.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorization.Desktop.Repositories.Categories;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    public async Task<int> CreateAsync(Category entity)
    {
        throw new System.NotImplementedException();
    }

    public Task<int> DeleteAsync(long id)
    {
        throw new System.NotImplementedException();
    }

    public Task<IList<CategoryViewModel>> GetAllAsync()
    {
        throw new System.NotImplementedException();
    }

    public Task<CategoryViewModel> GetByIdAsync(long id)
    {
        throw new System.NotImplementedException();
    }

    public Task<int> UpdateAsync(long id, Category entity)
    {
        throw new System.NotImplementedException();
    }
}
