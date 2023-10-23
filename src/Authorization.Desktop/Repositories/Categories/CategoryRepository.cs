using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Interfaces.Categories;
using Authorization.Desktop.ViewModels.Categories;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Authorization.Desktop.Repositories.Categories;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    public async Task<int> CreateAsync(Category entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO categories (name,shop_id, created_at, updated_at) VALUES (@Name,@ShopId, @Created_at, @Updated_at);";
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
            string query = $"Delete from categories where id= {id}";
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

    public async Task<IList<CategoryViewModel>> GetAllAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select * from categories;";
            var result = (await _connection.QueryAsync<CategoryViewModel>(query)).ToList();
            return result;

        }
        catch 
        {
            return new List<CategoryViewModel>();
        }
        finally 
        { 
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<CategoryViewModel>> GetAllByIdAsync(long shopId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Select * from categories where shop_id={shopId} order by id desc";
            var result = (await _connection.QueryAsync<CategoryViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<CategoryViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<CategoryViewModel> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Select * from categories where id={id}";
            var result = await _connection.QuerySingleAsync<CategoryViewModel>(query,new {Id = id});
            return result;
        }
        catch 
        {
            return new CategoryViewModel();
        }
        finally 
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Category entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Update categories set name=@Name,created_at=@Created_at, updated_at=@Updated_at where id={id};";
            var result = await _connection.ExecuteAsync(query,entity);
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
