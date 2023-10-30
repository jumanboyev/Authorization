using Authorization.Desktop.Repositories.Products;
using Authorization.Desktop.ViewModels.Products;
using Authorization.Desktop.Windows.Products;
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
        public Func<Task> Refresh;

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

        private async void btnproductUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(grStorage.SelectedItems.Count == 1)
            {
                if(grStorage.SelectedItem != null)
                {
                    var selectedItem = grStorage.SelectedItem as ProductAllToStorageViewModel;
                    StorageUpdateWindow storageUpdateWindow = new StorageUpdateWindow();
                    storageUpdateWindow.SetData(selectedItem!);
                    storageUpdateWindow.ShowDialog();
                    storageUpdateWindow.Refresh = RefreshAsync;
                }
            }
            else
            {
                MessageBox.Show("Yangilash uchun faqat bitta mahsulotni tanlang");
            }
        }

        private void btnAddQuantity_Click(object sender, RoutedEventArgs e)
        {
            ProductAddQuantityToStorage productAddQuantityToStorage = new ProductAddQuantityToStorage();
            productAddQuantityToStorage.ShowDialog();
        }

    }
}
