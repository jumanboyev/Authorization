using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Pages.Categories;
using Authorization.Desktop.Repositories.Shops;
using Authorization.Desktop.ViewModels.Shops;
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

namespace Authorization.Desktop.Components
{
    /// <summary>
    /// Interaction logic for ShopComponent.xaml
    /// </summary>
    public partial class ShopComponent : UserControl
    {
        public long ShopId { get; set; }
        private ShopViewModel viewodel;
        private ShopRepository _repository;
        public Func<Task> Refresh { get; set; }

        public ShopComponent()
        {
            InitializeComponent();
            this._repository = new ShopRepository();
        }

        public void SetData(ShopViewModel viewModel)
        {            
            this.viewodel = viewModel;
            lbName.Text = viewModel.Name;   
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Rostdan ham bu do'konni o'chirmoqchimisiz ?","O'chirish"
                ,MessageBoxButton.OKCancel,MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                var shopDelete = await _repository.DeleteAsync(viewodel.Id);
                await Refresh();

            }
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Shop shop = new Shop();            
            shop.Id = viewodel.Id;
            shop.Name = viewodel.Name;
            ShopUpdateWindow shopUpdateWindow = new ShopUpdateWindow();
            shopUpdateWindow.SetData(shop);
            shopUpdateWindow.ShowDialog();
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
                    if (button != null) button.Visibility = Visibility.Visible;
                    RadioButton? buttonBack = mainWindow.FindName("btnBackto") as RadioButton;

                    if (buttonBack != null)                    
                        buttonBack.Visibility = Visibility.Visible;



                }
            }
            // Get the parent Frame control
            Frame frame = FindParent<Frame>(this);

            // Navigate to the new page
            CategoryPage categoriesPage = new CategoryPage();
            categoriesPage.SetData(viewodel.Id, viewodel.Name);
            frame.Navigate(categoriesPage);
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
