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
            string query = "INSERT INTO categories (name, created_at, updated_at) VALUES (@Name, @Created_at, @Updated_at);";
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

    public Task<CategoryViewModel> GetByIdAsync(long id)
    {
        throw new System.NotImplementedException();
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
