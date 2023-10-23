using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Interfaces.Shops;
using Authorization.Desktop.ViewModels.Shops;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Desktop.Repositories.Shops;

public class ShopRepository : BaseRepository, IShopRepository
{
    public async Task<int> CreateAsync(Shop entity)
    {
        try
        {
            await _connection.OpenAsync();
                string query = "INSERT INTO shops (name,user_id, created_at, updated_at) " +
                                "VALUES (@Name,@UserId, @Created_at, @Updated_at);";
                    
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

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Delete from shops where id = {id}";
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

    public async Task<IList<ShopViewModel>> GetAllAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * from shops order by id desc;";
            var result = (await _connection.QueryAsync<ShopViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<ShopViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<ShopViewModel> GetByIdAsync(long id)
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> GetByIdShopNameAsync(string shopName)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Select * from shops where name = @Name";
            var result = await _connection.QuerySingleAsync<bool>(query,new {name = shopName});
            return result;
        }
        catch 
        {
            return false;
        }finally 
        { 
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Shop entity)
    {
        try
        {
            await _connection.OpenAsync();
            string quary = $"Update  shops set name=@Name,created_at=@Created_at, updated_at=@Updated_at where id={id};";
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
