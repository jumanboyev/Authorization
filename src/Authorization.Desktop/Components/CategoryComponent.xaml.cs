using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Interfaces;
using Authorization.Desktop.Pages.Categories;
using Authorization.Desktop.Pages.SubCategories;
using Authorization.Desktop.Repositories.Categories;
using Authorization.Desktop.ViewModels.Categories;
using Authorization.Desktop.Windows.Categories;
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
    /// Interaction logic for CategoryComponent.xaml
    /// </summary>
    public partial class CategoryComponent : UserControl
    {
        private long shopId { get; set; }
        private string shopName { get; set; } = string.Empty;

        private CategoryViewModel viewmodel;
        private CategoryRepository _repository;
        public Func<Task> Refresh { get; set; }
        public CategoryComponent()
        {
            InitializeComponent();
            this.viewmodel = new CategoryViewModel();
            this._repository = new CategoryRepository();
        }
        public void SetData(CategoryViewModel categoryViewModel)
        {
            viewmodel = categoryViewModel;
            lbName.Text = categoryViewModel.Name;
        }
        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Rostdan ham bu categoriyani o'chirmoqchimisiz ?", "O'chirish"
                , MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                var shopDelete = await _repository.DeleteAsync(viewmodel.Id);
                await Refresh();
            }
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Category category = new Category();
            category.Id = viewmodel.Id;
            category.ShopId = viewmodel.ShopId;
            category.Name = viewmodel.Name;
            UpdateCategoryWindow window = new UpdateCategoryWindow();
            window.SetaData(category);
            window.ShowDialog();
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
                    RadioButton? button = mainWindow.FindName("rbProduct") as RadioButton;
                    if (button != null) button.Visibility = Visibility.Collapsed;
                    RadioButton? buttonCategory = mainWindow.FindName("rbSubCategory") as RadioButton;
                    if (button != null) buttonCategory.Visibility = Visibility.Visible;
                    RadioButton? buttonBack = mainWindow.FindName("btnBackto") as RadioButton;

                    if (buttonBack != null)
                        buttonBack.Visibility = Visibility.Visible;

                }
            }
            // Get the parent Frame control
            Frame frame = FindParent<Frame>(this);

            // Navigate to the new page
            SubCategoryPage subCategoriesPage = new SubCategoryPage();
            subCategoriesPage.SetData(viewmodel.Id, viewmodel.Name);
            frame.Navigate(subCategoriesPage);
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
