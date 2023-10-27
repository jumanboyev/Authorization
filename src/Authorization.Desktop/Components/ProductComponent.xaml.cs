using Authorization.Desktop.Entities.Products;
using Authorization.Desktop.Repositories.Products;
using Authorization.Desktop.ViewModels.Products;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Authorization.Desktop.Components
{
    /// <summary>
    /// Логика взаимодействия для ProductComponent.xaml
    /// </summary>
    public partial class ProductComponent : UserControl
    {
        public Func<Task> Refresh { get; set; }

        private ProductViewModel viewModel;
        private ProductRepository _repository;

        public ProductComponent()
        {
            InitializeComponent();
            this.viewModel = new ProductViewModel();
            this._repository = new ProductRepository();
        }

        public void SetData(ProductViewModel productViewModel)
        {
            viewModel = productViewModel;
            lbBarCode.Text = productViewModel.BarCode.ToString();
            lbName.Text = productViewModel.Name;
            lbQuantity.Text = productViewModel.Quantity.ToString();
            lbSold_Price.Text = productViewModel.SoldPrice.ToString();
            lbPrice.Text = productViewModel.Price.ToString();
        }
        
        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            product.Id = viewModel.Id;
            product.SubCategoryId = viewModel.SubCategoryId;
            product.Name = viewModel.Name;
            product.BarCode = viewModel.BarCode;
            product.Quantity = viewModel.Quantity;  
            product.SoldPrice = viewModel.SoldPrice;
            product.Price = viewModel.Price;

            ProductUpdateWindow productUpdateWindow = new ProductUpdateWindow();
            productUpdateWindow.SetData(product);
            productUpdateWindow.ShowDialog();
            await Refresh();
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Rostdan ham bu productni o'chirmoqchimisiz ?", "O'chirish"
                , MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                var result = await _repository.DeleteAsync(viewModel.Id);
                await Refresh();
            }
        }

        private void B_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
