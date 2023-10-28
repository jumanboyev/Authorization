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
    /// Логика взаимодействия для ProductAddQuantityToStorage.xaml
    /// </summary>
    public partial class ProductAddQuantityToStorage : Window
    {
        public ProductAddQuantityToStorage()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtSoldPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
                
        }

        private void txtBarCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }


        private void txtQuantity_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
