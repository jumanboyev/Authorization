using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Entities.Subcategories;
using Authorization.Desktop.Interfaces.SubCategories;
using Authorization.Desktop.Pages;
using Authorization.Desktop.ViewModels.Categories;
using Authorization.Desktop.ViewModels.Shops;
using Authorization.Desktop.ViewModels.SubCategories;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Desktop.Repositories.SubCategories;

public class SubCategoryRepository : BaseRepository, ISubCategoryRepository
{
    public async Task<int> CreateAsync(SubCategory entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO subCategories (name,category_id, created_at, updated_at) " +
                            "VALUES (@Name,@CategoryId, @Created_at, @Updated_at);";

            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Delete from subCategories where id = {id}";
            var result = await _connection.ExecuteAsync(query);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<SubCategoryViewModel>> GetAllAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * from subCategories order by id desc;";
            var result = (await _connection.QueryAsync<SubCategoryViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<SubCategoryViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<SubCategoryViewModel>> GetAllByIdAsync(long CategoryId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Select * from subCategories where category_id={CategoryId} order by id desc";
            var result = (await _connection.QueryAsync<SubCategoryViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<SubCategoryViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<SubCategoryViewModel> GetByIdAsync(long id)
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> GetByIdSubCategoryName(string subCategoryName)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Select * from subCategories where name = @Name";
            var result = await _connection.QuerySingleAsync<bool>(query, new { name = subCategoryName });
            return result;
        }
        catch
        {
            return false;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, SubCategory entity)
    {
        try
        {
            await _connection.OpenAsync();
            string quary = $"Update  subCategories set name=@Name, updated_at=@Updated_at where id={id};";
            var result = await _connection.ExecuteAsync(quary, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
