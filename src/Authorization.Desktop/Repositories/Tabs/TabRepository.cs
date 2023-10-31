using Authorization.Desktop.Entities.Tabs;
using Authorization.Desktop.Interfaces.Tabs;
using Authorization.Desktop.ViewModels.SubCategories;
using Authorization.Desktop.ViewModels.Tabs;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Desktop.Repositories.Tabs;

public class TabRepository : BaseRepository, ITabRepository
{
    public async Task<int> CreateAsync(Tab entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO tabs (name,cashbox_id, created_at, updated_at) " +
                            "VALUES (@Name,@CashboxId, @Created_at, @Updated_at);";

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
            string query = $"Delete from tabs where id = {id}";
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

    public async Task<IList<TabViewModel>> GetAllAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * from tabs order by id desc;";
            var result = (await _connection.QueryAsync<TabViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<TabViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<TabViewModel>> GetAllByIdAsync(long cashboxId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Select * from tabs where cashbox_id={cashboxId} order by id desc";
            var result = (await _connection.QueryAsync<TabViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<TabViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<TabViewModel> GetByIdAsync(long id)
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> GetByIdTabName(long cashboxId, string tabName)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Select * from tabs where cashbox_id= {cashboxId} and name = @Name";
            var result = await _connection.QueryFirstOrDefaultAsync<bool>(query, new { name = tabName });
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

    public async Task<int> UpdateAsync(long id, Tab entity)
    {
        try
        {
            await _connection.OpenAsync();
            string quary = $"Update  tabs set name=@Name, updated_at=@Updated_at where id={id};";
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
