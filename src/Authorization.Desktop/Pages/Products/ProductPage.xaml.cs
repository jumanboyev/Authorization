using Authorization.Desktop.Components;
using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Entities.Subcategories;
using Authorization.Desktop.Pages.Categories;
using Authorization.Desktop.Pages.SubCategories;
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
            lbSubcategory.Content = subCategoryName;
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
                Height = 215,
                Width = 305,
                Style = (Style)FindResource("ProductCreateButton"),
                Margin = new Thickness(10, 10, 0, 0)
            };
            WpProduct.Children.Add(btn);
            btn.Click += btnCreate_Click;

            var subSategories = await _repository.GetAllByIdAsync(SubCategoryId);
            foreach (var subBategory in subSategories)
            {
                ProductComponent component = new ProductComponent();
                component.SetData(subBategory);
                WpProduct.Children.Add(component);
                component.Refresh = RefreshAsync;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                RadioButton? radioButton = mainWindow.FindName("rbShop") as RadioButton;
                if (radioButton != null)
                {
                    radioButton.Visibility = Visibility.Collapsed;
                    RadioButton? buttonBack = mainWindow.FindName("btnBackto") as RadioButton;

                    if (buttonBack != null)
                        buttonBack.Visibility = Visibility.Visible;

                }
            }
            // Get the parent Frame control
            Frame frame = FindParent<Frame>(this);

            // Navigate to the new page
            SubCategoryPage subCategoryPage = new SubCategoryPage();
            frame.Navigate(subCategoryPage);
        }
        private T? FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            if (parent == null)
                return null;

            T? typedParent = parent as T;
            return typedParent ?? FindParent<T>(parent);
        }
    }
}
