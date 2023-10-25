using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Entities.Products;
using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Interfaces.Products;
using Authorization.Desktop.Pages.SubCategories;
using Authorization.Desktop.ViewModels.Categories;
using Authorization.Desktop.ViewModels.Products;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Desktop.Repositories.Products;

public class ProductRepository : BaseRepository, IProductRepository
{
    public async Task<int> CreateAsync(Product entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO products ( name,subcategory_id, bar_code,quantity,sold_price,price, created_at, updated_at) " +
                "VALUES ( @Name,@SubCategoryId, @BarCode, @Quantity, @SoldPrice, @Price, @Created_at, @Updated_at);";
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
            string query = $"Delete from products where id = {id};";
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

    public async Task<IList<ProductViewModel>> GetAllAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select * from products;";
            var result = (await _connection.QueryAsync<ProductViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<ProductViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<ProductViewModel>> GetAllByIdAsync(long subCategoryId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Select * from products where subcategory_id={subCategoryId} order by id desc";
            var result = (await _connection.QueryAsync<ProductViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<ProductViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<ProductViewModel> GetByIdAsync(long id)
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> GetByIdProductNameAsync(long subCategoryId, string productName)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Select * from products where subcategory_id={subCategoryId} and name = @Name;";
            var result = await _connection.QueryFirstOrDefaultAsync<bool>(query,new {name = productName });
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

    public async Task<int> UpdateAsync(long id, Product entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Update products set subcategory_id = @SubCategoryId,name = @Name,bar_code = @BarCode,quantity = @Quantity, " +
                $"sold_price=@SoldPrice,price=@Price, updated_at = @Updated_at where id = {id}";
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
