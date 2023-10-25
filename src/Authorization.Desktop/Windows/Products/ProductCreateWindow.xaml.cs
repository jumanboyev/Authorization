using Authorization.Desktop.Entities.Products;
using Authorization.Desktop.Helpers;
using Authorization.Desktop.Repositories.Products;
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
    /// Логика взаимодействия для ProductCreateWindow.xaml
    /// </summary>
    public partial class ProductCreateWindow : Window
    {
        private ProductRepository _repository;

        public long subCategoryId { get; set; }
        public ProductCreateWindow()
        {
            InitializeComponent();
            this._repository = new ProductRepository();
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            int count =0;

            if(txtName.Text.Length == 0 && txtBarCode.Text.Length == 0 && txtSoldPrice.Text.Length == 0 && txtPrice.Text.Length == 0 )
            {
                MessageBox.Show("Maydonlar bo'sh bo'lishi mumkin emas!");
                return;
            }

            if (txtName.Text.Length >= 3) count++;
            else 
            { 
                MessageBox.Show("Maxsulot nomi uzunligi kamida 3 ta bo'lishi kerak.");
                return;
            }

            if(txtBarCode.Text.Length == 13) count++;
            else
            {
                MessageBox.Show("Shtrix kod uzunligi 13 ta bo'lishi kerak!");
                return;
            }     
        
            if(count == 2)
            {
                Product product = new Product();
                product.SubCategoryId = this.subCategoryId;
                product.Name = txtName.Text;
                product.Barcode = long.Parse(txtBarCode.Text);
                product.Quantity  = long.Parse(txtQuantity.Text);
                product.SoldPrice = float.Parse(txtSoldPrice.Text);
                product.Price = float.Parse(txtPrice.Text);
                product.Created_at = TimeHelper.GetDateTime();
                product.Updated_at = TimeHelper.GetDateTime();  

                var products = await _repository.CreateAsync(product);
                if (products > 0)
                {
                    MessageBox.Show("Muvaffaqiyatli yaratildi");
                }
                else
                {
                    MessageBox.Show("Xatoli");
                }
            }
        }

        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void GetSubCategoryId(long id)
        {
            this.subCategoryId = id;
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
