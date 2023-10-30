using Authorization.Desktop.Entities.Cashboxes;
using Authorization.Desktop.Interfaces.Cashboxes;
using Authorization.Desktop.Pages.Categories;
using Authorization.Desktop.ViewModels.Cashboxes;
using Authorization.Desktop.ViewModels.Categories;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Desktop.Repositories.Cashboxes;

public class CashboxRepository : BaseRepository, ICashboxRepository
{
    public async Task<int> CreateAsync(Cashbox entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Insert Into cashbox(name,created_at,updated_at) Values(@Name,@Created_at,@Updated_at);";
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
            string query = $"Delete from cashbox where id= {id}";
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

    public async Task<IList<CashboxViewModel>> GetAllAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select * from cashbox order by id desc;";
            var result = (await _connection.QueryAsync<CashboxViewModel>(query)).ToList();
            return result;

        }
        catch
        {
            return new List<CashboxViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<CashboxViewModel> GetByIdAsync(long id)
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> GetByIdCashboxName(string CashboxName)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Select * from cashbox where name = @Name";
            var result = await _connection.QuerySingleOrDefaultAsync<bool>(query, new { name = CashboxName });
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

    public async Task<int> UpdateAsync(long id, Cashbox entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Update cashbox set name=@Name, updated_at=@Updated_at where id={id};";
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
}
