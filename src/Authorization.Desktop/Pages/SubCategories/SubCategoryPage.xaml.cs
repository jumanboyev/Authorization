using Authorization.Desktop.Components;
using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Interfaces;
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

        private SubCategoryRepository _repository;

        public SubCategoryPage()
        {
            InitializeComponent();
            this._repository = new SubCategoryRepository();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }
        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            SubCategoryCreateWindow window = new SubCategoryCreateWindow();
            window.getShopId(CategoryId);
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

            var categories = await _repository.GetAllByIdAsync(CategoryId);
            foreach (var category in categories)
            {
                SubCategoryComponents component = new SubCategoryComponents();
                component.SetData(category);
                WpSubCategory.Children.Add(component);
                component.Refresh = RefreshAsync;
            }
        }
    }
}
