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
            Product product = new Product();

            if (txtName.Text.Length == 0 )
            {
                MessageBox.Show("Name bo'sh bo'lishi mumkin emas!");
                return;
            }
            if (txtQuantity.Text.Length == 0)
            {
                MessageBox.Show("Quantity bo'sh bo'lishi mumkin emas!");
                return;
            }
            else
            {
                string number="";
                foreach (var item in txtQuantity.Text.Split(","))
                {
                    number += item;
                }

                product.Quantity = long.Parse(number);
            }

            if (txtSoldPrice.Text.Length == 0 )
            {
                MessageBox.Show("SoldPrice maydon bo'sh bo'lishi mumkin emas!");
                return;
            }
            else
            {
                product.SoldPrice = float.Parse(txtSoldPrice.Text);
            }

            if ( txtPrice.Text.Length == 0)
            {
                MessageBox.Show("Price maydonlar bo'sh bo'lishi mumkin emas!");
                return;
            }
            else
            {
                product.Price = float.Parse(txtPrice.Text);
            }


            if (txtName.Text.Length >= 3 )
            {
                product.Name = txtName.Text;
                count++;
            }
            else
            {
                MessageBox.Show("Maxsulot nomi uzunligi kamida 3 ta bo'lishi kerak.");
                return;
            }

            if (txtBarCode.Text.Length == 13 || txtBarCode.Text.Length == 0) 
            { 
                if(txtBarCode.Text.Length == 0)
                {
                    Guid guid = Guid.NewGuid();
                    long number = BitConverter.ToInt64(guid.ToByteArray(), 0);
                    number = Math.Abs(number); 
                    string thirteenDigitNumber = number.ToString().Substring(0, 13);
                    product.BarCode = long.Parse(thirteenDigitNumber.ToString());
                }
                else
                {
                    product.BarCode = long.Parse(txtBarCode.Text);
                }
                count++;
            }
            else
            {
                MessageBox.Show("Shtrix kod uzunligi 13 ta bo'lishi yoki maydon bo'sh bo'lishi kerak!");
                return;
            }

            var isProductName = await _repository.GetByIdProductNameAsync(subCategoryId, product.Name);

            if (isProductName)
            {
                MessageBox.Show("Bunday Product allaqachon yaratilgan");
                return;
            }
            else count++;
            

            if (count == 3)
            {
                product.SubCategoryId = this.subCategoryId;
                product.Created_at = TimeHelper.GetDateTime();
                product.Updated_at = TimeHelper.GetDateTime();  

                var products = await _repository.CreateAsync(product);
                if (products > 0)
                {
                    MessageBox.Show("Muvaffaqiyatli yaratildi");
                    this.Close();
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

        private void txtSoldPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = textBox.Text.TrimStart('0');

                textBox.Text = textBox.Text.Replace(".", "");

                if (!string.IsNullOrEmpty(textBox.Text) && double.TryParse(textBox.Text, out double number))
                {
                    textBox.TextChanged -= txtSoldPrice_TextChanged; // Remove event handler temporarily

                    textBox.Text = number.ToString("#,##0").Replace(","," ");// Apply formatting with dots

                    textBox.CaretIndex = textBox.Text.Length; // Set caret position to end

                    textBox.TextChanged += txtSoldPrice_TextChanged; // Reattach event handler
                }

            }
        }
    }
}
