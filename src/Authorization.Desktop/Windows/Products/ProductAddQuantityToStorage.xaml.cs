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
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Authorization.Desktop.Windows.Products
{
    /// <summary>
    /// Логика взаимодействия для ProductAddQuantityToStorage.xaml
    /// </summary>
    public partial class ProductAddQuantityToStorage : Window
    {
        public long Id { get; set; }
        public long SubCategoryId { get; set; }
        public long oldQuantity { get; set; }


        private ProductRepository _repository;
        private ProductAllToStorageViewModel viewmodel;

        public ProductAddQuantityToStorage()
        {
            InitializeComponent();
            this._repository = new ProductRepository();
            this.viewmodel = new ProductAllToStorageViewModel();
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
            lbName.Text = productAllToStorageViewModel.Name;
            lbOldQuantity.Text = FPrice(productAllToStorageViewModel.Quantity.ToString());

            this.oldQuantity = long.Parse(productAllToStorageViewModel.Quantity.ToString());
        }
        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string newQuantityString = "";
            foreach (var item in txtQuantity.Text.Split(","))
            {
                newQuantityString += item;
            }

            if(txtQuantity.Text.Length == 0)
            {
                MessageBox.Show("Quantity bo'sh bo'lishi mumkin emas!");
                return;
            }

            long newQuantity = long.Parse(newQuantityString)+oldQuantity;
            var dbResult = await _repository.AddQuantity(Id, newQuantity);

            if (dbResult > 0)
            {
                MessageBox.Show("Muvaffaqiyatli qo'shildi");
                this.Close();
            }
            else
            {
                MessageBox.Show("Xatoli");
            }

        }

        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtBarCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9]"))
            {
                e.Handled = true;
            }
        }


        private void txtQuantity_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = textBox.Text.TrimStart('0');

                textBox.Text = textBox.Text.Replace(".", "");

                if (!string.IsNullOrEmpty(textBox.Text) && double.TryParse(textBox.Text, out double number))
                {
                    textBox.TextChanged -= txtQuantity_TextChanged;

                    textBox.Text = number.ToString("#,##0");

                    textBox.CaretIndex = textBox.Text.Length;

                    textBox.TextChanged += txtQuantity_TextChanged;
                }

            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
