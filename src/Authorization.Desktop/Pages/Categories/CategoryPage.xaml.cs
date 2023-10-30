using Authorization.Desktop.Components;
using Authorization.Desktop.Interfaces;
using Authorization.Desktop.Pages.SubCategories;
using Authorization.Desktop.Repositories.Categories;
using Authorization.Desktop.Windows.Categories;
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
using System.Xml.Linq;

namespace Authorization.Desktop.Pages.Categories
{
    /// <summary>
    /// Interaction logic for CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : Page
    {
        public long shopId { get; set; }
        private string shopName { get; set; } = string.Empty;

        private CategoryRepository _repository;

        public CategoryPage()
        {
            InitializeComponent();
            this._repository = new CategoryRepository();
        }

        public void SetData(long shopId, string ShopName)
        {
            this.shopId = shopId;
            this.shopName = ShopName;
            lbShop.Content = ShopName;
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }
        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateCategoryWindow window = new CreateCategoryWindow();
            window.getShopId(shopId);
            window.ShowDialog();
            await RefreshAsync();
        }
        public async Task RefreshAsync()
        {
            WpCategory.Children.Clear();
            Button btn = new Button()
            {
                Name = "btnCreate",
                Visibility = Visibility.Visible,
                Height = 120,
                Width = 200,
                Style = (Style)FindResource("ProductCreateButton"),
                Margin = new Thickness(10, 10, 0, 0)
            };
            WpCategory.Children.Add(btn);
            btn.Click += btnCreate_Click;

            var categories = await _repository.GetAllAsync();
            foreach (var category in categories)
            {
                CategoryComponent component = new CategoryComponent();
                component.SetData(category,shopName);
                WpCategory.Children.Add(component);
                component.Refresh = RefreshAsync;
            }   
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
        
    }
}
