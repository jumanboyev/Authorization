using Authorization.Desktop.Components;
using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Interfaces;
using Authorization.Desktop.Pages.Categories;
using Authorization.Desktop.Repositories.SubCategories;
using Authorization.Desktop.Windows.Categories;
using Authorization.Desktop.Windows.SubCategories;
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

namespace Authorization.Desktop.Pages.SubCategories
{
    /// <summary>
    /// Логика взаимодействия для SubCategoryPage.xaml
    /// </summary>
    public partial class SubCategoryPage : Page
    {
        public long CategoryId { get; set; }
        private string CategoryName { get; set; } = string.Empty;
        private string ShopName { get; set; } = string.Empty;

        private SubCategoryRepository _repository;

        public SubCategoryPage()
        {
            InitializeComponent();
            this._repository = new SubCategoryRepository();
        }
        public void SetData(long categoryId, string categoryName,string shopName)
        {
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
            this.ShopName = shopName;
            lbCategory.Content = CategoryName;
            lbShop.Content = ShopName;
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }
        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            SubCategoryCreateWindow window = new SubCategoryCreateWindow();
            window.getCategoryId(CategoryId);
            window.ShowDialog();
            await RefreshAsync();
        }
        public async Task RefreshAsync()
        {
            WpSubCategory.Children.Clear();
            Button btn = new Button()
            {
                Name = "btnCreate",
                Visibility = Visibility.Visible,
                Height = 120,
                Width = 200,
                Style = (Style)FindResource("ProductCreateButton"),
                Margin = new Thickness(10, 10, 0, 0)
            };
            WpSubCategory.Children.Add(btn);
            btn.Click += btnCreate_Click;

            var subSategories = await _repository.GetAllByIdAsync(CategoryId);
            foreach (var subBategory in subSategories)
            {
                SubCategoryComponents component = new SubCategoryComponents();
                component.SetData(subBategory,CategoryName,ShopName);
                WpSubCategory.Children.Add(component);
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
