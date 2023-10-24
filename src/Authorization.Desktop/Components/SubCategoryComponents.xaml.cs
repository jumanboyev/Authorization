using Authorization.Desktop.Entities.Subcategories;
using Authorization.Desktop.Pages.Categories;
using Authorization.Desktop.Pages.Products;
using Authorization.Desktop.Repositories.SubCategories;
using Authorization.Desktop.ViewModels.Categories;
using Authorization.Desktop.ViewModels.SubCategories;
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

namespace Authorization.Desktop.Components
{
    /// <summary>
    /// Логика взаимодействия для SubCategoryComponents.xaml
    /// </summary>
    public partial class SubCategoryComponents : UserControl
    {
        public Func<Task> Refresh { get; set; }

        private SubCategoryRepository _repository;
        private SubCategoryViewModel viewModel;

        public SubCategoryComponents()
        {
            InitializeComponent();
            this._repository = new SubCategoryRepository();
            this.viewModel = new SubCategoryViewModel();
        }

        public void SetData(SubCategoryViewModel subCategoryViewModel)
        {
            viewModel = subCategoryViewModel;
            lbName.Text = subCategoryViewModel.Name;
        }
        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Rostdan ham bu categoriyani o'chirmoqchimisiz ?", "O'chirish"
                , MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                var shopDelete = await _repository.DeleteAsync(viewModel.Id);
                await Refresh();
            }
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            SubCategory subCategory = new SubCategory();
            subCategory.Id = viewModel.Id;
            subCategory.CategoryId = viewModel.CategoryId;
            subCategory.Name = viewModel.Name;

            SubCategoryUpdateWindow subCategoryUpdateWindow = new SubCategoryUpdateWindow();
            subCategoryUpdateWindow.SetaData(subCategory);
            subCategoryUpdateWindow.ShowDialog();
            await Refresh();

        }

        private void B_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                RadioButton? radioButton = mainWindow.FindName("rbShop") as RadioButton;
                if (radioButton != null)
                {
                    radioButton.Visibility = Visibility.Collapsed;
                    RadioButton? button = mainWindow.FindName("rbCategory") as RadioButton;
                    if (button != null) button.Visibility = Visibility.Collapsed;

                    RadioButton? buttonSubCategory = mainWindow.FindName("rbSubCategory") as RadioButton;
                    if (button != null) buttonSubCategory!.Visibility = Visibility.Collapsed;

                    RadioButton? buttonProduct = mainWindow.FindName("rbProduct") as RadioButton;
                    if (button != null) buttonProduct!.Visibility = Visibility.Visible;

                    RadioButton? buttonBack = mainWindow.FindName("btnBackto") as RadioButton;
                    if (buttonBack != null)
                        buttonBack.Visibility = Visibility.Visible;

                }
            }
            // Get the parent Frame control
            Frame frame = FindParent<Frame>(this)!;

            // Navigate to the new page
            ProductPage productPage = new ProductPage();
            productPage.SetData(viewModel.Id, viewModel.Name);
            frame.Navigate(productPage);
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
