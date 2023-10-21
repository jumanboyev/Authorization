using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.ViewModels.Categories;

namespace Authorization.Desktop.Interfaces.Categories;

public interface ICategoryRepository:IRepository<Category,CategoryViewModel>
{
}
