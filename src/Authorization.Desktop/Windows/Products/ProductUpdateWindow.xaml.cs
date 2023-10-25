using Authorization.Desktop.Entities.Products;
using Authorization.Desktop.Helpers;
using Authorization.Desktop.Repositories.Products;
using Authorization.Desktop.ViewModels.Products;
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

namespace Authorization.Desktop.Windows.Products
{
    /// <summary>
    /// Логика взаимодействия для ProductUpdateWindow.xaml
    /// </summary>
    public partial class ProductUpdateWindow : Window
    {
        public long Id { get; set; }
        public long SubCategoryId { get; set; }
        private ProductRepository _repository;

        public ProductUpdateWindow()
        {
            InitializeComponent();
            this._repository = new ProductRepository();
            
        }

        public void SetData(Product product)
        {
            this.Id = product.Id;
            this.SubCategoryId = product.SubCategoryId;
            txtName.Text = product.Name;
            txtBarCode.Text = product.BarCode.ToString();
            txtQuantity.Text = product.Quantity.ToString();
            txtSoldPrice.Text = product.SoldPrice.ToString();
            txtPrice.Text = product.Price.ToString();
        }
        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            Product product = new Product();

            if (txtName.Text.Length == 0 && txtBarCode.Text.Length == 0 && txtSoldPrice.Text.Length == 0 && txtPrice.Text.Length == 0)
            {
                MessageBox.Show("Maydonlar bo'sh bo'lishi mumkin emas!");
                return;
            }

            if (txtName.Text.Length >= 3)
            {
                product.Name = txtName.Text;
                count++;
            }
            else
            {
                MessageBox.Show("Maxsulot nomi uzunligi kamida 3 ta bo'lishi kerak.");
                return;
            }

            if (txtBarCode.Text.Length == 13) count++;
            else
            {
                MessageBox.Show("Shtrix kod uzunligi 13 ta bo'lishi kerak!");
                return;
            }
            var isProductName = await _repository.GetByIdProductNameAsync(SubCategoryId, product.Name);

            if(isProductName)
            {
                MessageBox.Show("Bunday Product allaqachon bor");
                return;
            }
            else count++;
            
            if (count == 3)
            {
                product.SubCategoryId = SubCategoryId;
                product.BarCode = long.Parse(txtBarCode.Text);
                product.Quantity = long.Parse(txtQuantity.Text);
                product.SoldPrice = float.Parse(txtSoldPrice.Text);
                product.Price = float.Parse(txtPrice.Text);
                product.Updated_at = TimeHelper.GetDateTime();

                var products = await _repository.UpdateAsync(Id,product);
                if (products > 0)
                {
                    MessageBox.Show("Muvaffaqiyatli tahrirlandi");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Xatoli");
                }
            }
        }

        private void txtBarCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9]"))
            {
                e.Handled = true;
            }
        }

        private void txtBarCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
