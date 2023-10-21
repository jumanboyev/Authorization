    using Authorization.Desktop.Entities.Users;
using Authorization.Desktop.Interfaces.Users;
using Authorization.Desktop.ViewModels.Users;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Desktop.Repositories.Users;

public class UserRepository : BaseRepository, IUserRepository
{
    public async Task<int> CreateAsync(User entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO users (username, password, salt, created_at, updated_at) " +
                           "VALUES (@UserName, @Password, @Salt, @Created_at,@Updated_at);";
            var result = await _connection.ExecuteAsync(query,entity);
            return result;
        }
        catch (System.Exception)
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
            string query = $"Delete  from users  Where id ={id};";
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

    public Task<IList<UserViewModel>> GetAllAsync()
    {
        throw new System.NotImplementedException();
    }

    public async Task<UserViewModel> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT id, username,password,salt, created_at, updated_at " +
                $" FROM users where id = {id};";

            var result = await _connection.QuerySingleAsync<UserViewModel>(query, new { id = id });

            return result;
        }
        catch
        {
            return new UserViewModel();
        }
        finally
        {
           await _connection.CloseAsync();
        }
    }

    public async Task<IList<User>> GetByIdUserName(string UserName)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * from users where username = @UserName;";

            var result =(await _connection.QueryAsync<User>(query,new {username = UserName})).ToList();
            return result;
        }
        catch 
        {
            return new List<User>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<int> UpdateAsync(long id, User entity)
    {
        throw new System.NotImplementedException();
    }
}
