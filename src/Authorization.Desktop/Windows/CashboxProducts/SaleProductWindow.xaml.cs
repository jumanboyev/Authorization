using Authorization.Desktop.Components;
using Authorization.Desktop.Entities.SaleProduct;
using Authorization.Desktop.Interfaces;
using Authorization.Desktop.Repositories.Products;
using Authorization.Desktop.Repositories.SaleProducts;
using Authorization.Desktop.ViewModels.Products;
using Authorization.Desktop.ViewModels.SaleProducts;
using Authorization.Desktop.Windows.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Authorization.Desktop.Windows.CashboxProducts
{
    /// <summary>
    /// Логика взаимодействия для SaleProductWindow.xaml
    /// </summary>
    public partial class SaleProductWindow : Window
    {
        public long TabId {  get; set; }
        private SaleProductRepository _repository;
        private ProductRepository _productRepository;

        public SaleProductWindow()
        {
            InitializeComponent();
            this._repository = new SaleProductRepository();
            this._productRepository = new ProductRepository();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }

        public async Task RefreshAsync()
        {
            var result = await _productRepository.GetAllProductToStorage();
            grSaleProduct.ItemsSource = result;
        }

        public void getTabId(long tabId)
        {
            this.TabId = tabId;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private async void grSaleProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                if (grSaleProduct.SelectedItem != null)
                {

                    var selectedItem = grSaleProduct.SelectedItem as ProductAllToStorageViewModel;

                    SaleProduct saleProduct = new SaleProduct();
                    saleProduct.TabId = TabId;
                    saleProduct.Name = selectedItem!.Name;
                    saleProduct.BarCode = selectedItem.BarCode;
                    saleProduct.Quantity = selectedItem.Quantity;
                    saleProduct.SoldPrice = selectedItem.SoldPrice;
                    saleProduct.Price = selectedItem.Price;
                    saleProduct.Category = selectedItem.Category;
                    saleProduct.Subcategory = selectedItem.Subcategory;

                    var isProduct = await _repository.GetByIdSaleProductNameAsync(TabId,saleProduct.Name);
                    
                    if(!isProduct)
                    {
                        var result = await _repository.CreateSaleProductAsync(saleProduct);

                        if (result == 0) MessageBox.Show("Xatoli");
                        else
                        {
                            MessageBox.Show("Muvaffaqiyatli tanlandi");
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bunday product tanlangan !");
                    }
                }
            }
            else
            {
                MessageBox.Show("1 ta mahsulotni tanlang");
            }

        }
    }
}
