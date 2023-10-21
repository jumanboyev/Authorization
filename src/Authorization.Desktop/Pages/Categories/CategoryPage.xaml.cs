using Authorization.Desktop.Components;
using Authorization.Desktop.Interfaces;
using Authorization.Desktop.Repositories.Categories;
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

namespace Authorization.Desktop.Pages.Categories
{
    /// <summary>
    /// Interaction logic for CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : Page
    {
        private CategoryRepository _repository;

        public CategoryPage()
        {
            InitializeComponent();
            this._repository = new CategoryRepository();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            ShopCreateWindow window = new ShopCreateWindow();
            window.ShowDialog();
            await RefreshAsync();
        }
        public async Task RefreshAsync()
        {
            MainWP.Children.Clear();
            Button btn = new Button()
            {
                Name = "btnCreate",
                Visibility = Visibility.Visible,
                Content = "+",
                Height = 120,
                Width = 200,
                Style = (Style)FindResource("ProductCreateButton"),
                Margin = new Thickness(10, 10, 0, 0)

            };
            MainWP.Children.Add(btn);
            btn.Click += btnCreate_Click;

            var categories = await _repository.GetAllAsync();
            foreach (var category in categories)
            {
                CategoryComponent component = new CategoryComponent();
               // component.SetData(shop);
                MainWP.Children.Add(component);
                component.Refresh = RefreshAsync;
            }
        }

    }
}
