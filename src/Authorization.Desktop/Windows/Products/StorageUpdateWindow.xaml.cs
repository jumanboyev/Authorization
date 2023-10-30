using Authorization.Desktop.Entities.Products;
using Authorization.Desktop.Entities.Subcategories;
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
    /// Interaction logic for StorageUpdateWindow.xaml
    /// </summary>
    public partial class StorageUpdateWindow : Window
    {
        public long Id { get; set; }
        public long SubCategoryId { get; set; }

        private ProductRepository _repository;
        private ProductAllToStorageViewModel viewModel;

        public StorageUpdateWindow()
        {
            InitializeComponent();
            this._repository = new ProductRepository();
            this.viewModel = new ProductAllToStorageViewModel();
        }
        public string FPrice(string numbers)
        {
            long number = long.Parse(numbers);
            string FNumber = number.ToString("#,##0");
            return FNumber;
        }
        public void SetData(ProductAllToStorageViewModel productAllToStorageViewModel)
        {
            this.Id = productAllToStorageViewModel.Id;
            this.SubCategoryId = productAllToStorageViewModel.SubCategoryId;
            lbCategory.Text = productAllToStorageViewModel.Category;
            lbSubcategory.Text = productAllToStorageViewModel.Subcategory;
            lbBarCode.Text = productAllToStorageViewModel.BarCode.ToString();
            txtName.Text = productAllToStorageViewModel.Name;

            txtQuantity.Text = FPrice(productAllToStorageViewModel.Quantity.ToString());
            txtSoldPrice.Text = FPrice(productAllToStorageViewModel.SoldPrice.ToString());
            txtPrice.Text = FPrice(productAllToStorageViewModel.Price.ToString());
        }

        private void txtSoldPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = textBox.Text.TrimStart('0');

                textBox.Text = textBox.Text.Replace(".", "");

                if (!string.IsNullOrEmpty(textBox.Text) && double.TryParse(textBox.Text, out double number))
                {
                    textBox.TextChanged -= txtSoldPrice_TextChanged;

                    textBox.Text = number.ToString("#,##0");

                    textBox.CaretIndex = textBox.Text.Length;

                    textBox.TextChanged += txtSoldPrice_TextChanged;
                }

            }
        }

        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            Product product = new Product();

            if (txtQuantity.Text.Length == 0)
            {
                MessageBox.Show("Quantity bo'sh bo'lishi mumkin emas!");
                return;
            }
            else
            {
                string number = "";
                foreach (var item in txtQuantity.Text.Split(","))
                {
                    number += item;
                }

                product.Quantity = long.Parse(number);
            }

            if (txtSoldPrice.Text.Length == 0)
            {
                MessageBox.Show("SoldPrice maydon bo'sh bo'lishi mumkin emas!");
                return;
            }
            else
            {
                string number = "";
                foreach (var item in txtSoldPrice.Text.Split(","))
                {
                    number += item;
                }

                product.SoldPrice = long.Parse(number);
            }

            if (txtPrice.Text.Length == 0)
            {
                MessageBox.Show("Price maydonlar bo'sh bo'lishi mumkin emas!");
                return;
            }
            else
            {
                string number = "";
                foreach (var item in txtPrice.Text.Split(","))
                {
                    number += item;
                }

                product.Price = long.Parse(number);
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

            if (count == 1)
            {
                product.SubCategoryId = SubCategoryId;
                product.BarCode = long.Parse(lbBarCode.Text);
                product.Updated_at = TimeHelper.GetDateTime();

                var products = await _repository.UpdateAsync(Id, product);
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

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
