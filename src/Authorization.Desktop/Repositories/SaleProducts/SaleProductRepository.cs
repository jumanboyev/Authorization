using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Entities.SaleProduct;
using Authorization.Desktop.Interfaces.SaleProducts;
using Authorization.Desktop.Pages.SubCategories;
using Authorization.Desktop.ViewModels.SaleProducts;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Desktop.Repositories.SaleProducts;

public class SaleProductRepository : BaseRepository, ISaleProductRepository
{
    public async Task<int> CreateSaleProductAsync(SaleProduct saleProduct)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO saleproduct(name, tab_id,bar_code,quantity,sold_price,price, category, subcategory) " +
                "VALUES (@Name, @TabId ,@BarCode,@Quantity,@SoldPrice,@Price,@Category,@Subcategory)";
            var result = await _connection.ExecuteAsync(query, saleProduct);
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

    public async Task<IList<SaleProductViewModel>> GetAllSaleProductsAsync(long tabId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * from saleproduct where tab_id = {tabId} order by id desc;";
            var result = (await _connection.QueryAsync<SaleProductViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<SaleProductViewModel>();
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<bool> GetByIdSaleProductNameAsync(long tabId, string productName)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Select * from saleproduct where tab_id={tabId} and name = @Name";
            var result = await _connection.QueryFirstOrDefaultAsync<bool>(query, new { name = productName });
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
}
