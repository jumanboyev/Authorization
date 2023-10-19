using Authorization.Desktop.Components;
using Authorization.Desktop.Repositories.Shops;
using Authorization.Desktop.Windows.Shops;
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

namespace Authorization.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for ShopPage.xaml
    /// </summary>
    public partial class ShopPage : Page
    {
        private ShopRepository _repository;

        public ShopPage()
        {
            this._repository = new ShopRepository();
            InitializeComponent();
        }

        Button btn = new Button();
       
        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            ShopCreateWindow window = new ShopCreateWindow();
            window.ShowDialog();
            await RefreshAsync();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }

        public async Task RefreshAsync()
        {
            MainWP.Children.Clear();
            var shops = await _repository.GetAllAsync();
            foreach (var shop in shops)
            {
                ShopComponent component = new ShopComponent();
                component.SetData(shop);
                MainWP.Children.Add(component);
                component.Refresh = RefreshAsync;
            }
        }
    }
}
