using Authorization.Desktop.Components;
using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Entities.Subcategories;
using Authorization.Desktop.Repositories.Products;
using Authorization.Desktop.ViewModels.Products;
using Authorization.Desktop.Windows.Products;
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

namespace Authorization.Desktop.Pages.Products
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public long SubCategoryId { get; set; }
        public string SubCategoryName { get; set; } = string.Empty;

        private ProductRepository _repository;
        private ProductViewModel viewModel;

        public ProductPage()
        {
            InitializeComponent();
            this._repository = new ProductRepository();
            this.viewModel = new ProductViewModel();
        }
        public void SetData(long subCategoryId,string subCategoryName)
        {
            this.SubCategoryId = subCategoryId;
            this.SubCategoryName = subCategoryName;
            lbSubCategory.Content = subCategoryName;
        }
        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            ProductCreateWindow window = new ProductCreateWindow();
            window.GetSubCategoryId(SubCategoryId);
            window.ShowDialog();
            await RefreshAsync();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }

        public async Task RefreshAsync()
        {
            WpProduct.Children.Clear();
            Button btn = new Button()
            {
                Name = "btnCreate",
                Visibility = Visibility.Visible,
                Height = 220,
                Width = 200,
                Style = (Style)FindResource("ProductCreateButton"),
                Margin = new Thickness(10, 10, 0, 0)
            };
            WpProduct.Children.Add(btn);
            btn.Click += btnCreate_Click;

            var subSategories = await _repository.GetAllByIdAsync(SubCategoryId);
            foreach (var subBategory in subSategories)
            {
                SubCategoryComponents component = new SubCategoryComponents();
                //component.SetData(subBategory);
                WpProduct.Children.Add(component);
                component.Refresh = RefreshAsync;
            }
        }
    }
}
