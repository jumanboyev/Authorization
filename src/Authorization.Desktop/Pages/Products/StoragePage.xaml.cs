using Authorization.Desktop.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Authorization.Desktop.Pages.Products
{
    /// <summary>
    /// Interaction logic for StoragePage.xaml
    /// </summary>
    public partial class StoragePage : Page
    {
        private ProductRepository _repository;

        public StoragePage()
        {
            InitializeComponent();
            this._repository = new ProductRepository();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshAsync();
        }

        public async void RefreshAsync()
        {
            var result = await _repository.GetAllProductToStorage();
            grStorage.ItemsSource = result;
        }

    }
}
