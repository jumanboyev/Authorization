using Authorization.Desktop.Components;
using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Entities.Tabs;
using Authorization.Desktop.Repositories.Tabs;
using Authorization.Desktop.ViewModels.Cashboxes;
using Authorization.Desktop.Windows.Categories;
using Authorization.Desktop.Windows.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Authorization.Desktop.Windows.CashboxProducts
{
    /// <summary>
    /// Логика взаимодействия для CashboxProductWindow.xaml
    /// </summary>
    public partial class CashboxProductWindow : Window, INotifyPropertyChanged
    {
        public long cashboxId {  get; set; }
        public long tabId {  get; set; }

        private TabRepository _repository;
        private CashboxViewModel viewModel;

        public event PropertyChangedEventHandler? PropertyChanged;
        
        public delegate void OpenMainWindowDelegate();
        public OpenMainWindowDelegate? OpenWindow { get; set; }

        public CashboxProductWindow()
        {
            InitializeComponent();
            this._repository = new TabRepository();
            this.viewModel = new CashboxViewModel();
        }

        public void SetData(CashboxViewModel cashboxViewModel)
        {
            viewModel = cashboxViewModel;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            TabCreateWindow window = new TabCreateWindow();
            window.getCashboxId(viewModel.Id);
            window.ShowDialog();
            await RefreshAsync();
            
        }
        private async void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            CreateCategoryWindow window = new CreateCategoryWindow();
            // window.getShopId(shopId);
            window.ShowDialog();
            await RefreshAsync();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
            await RefreshAsyncAddProduct();
        }

        private void YourUserControl_UserControlClicked(long id)
        {
            this.tabId = id;

        }
        public async Task RefreshAsync()
        {
            wpTab.Children.Clear();
            tabId = 0;
            Button btn = new Button()
            {
                Name = "btnCreate",
                Visibility = Visibility.Visible,
                Height = 40,
                Width = 75,
                Style = (Style)FindResource("TabCreateButton"),
                Margin = new Thickness(10, 15, 0, 0)
            };

            wpTab.Children.Add(btn);
            btn.Click += btnCreate_Click;

            var tabs = await _repository.GetAllByIdAsync(viewModel.Id);

            foreach ( var tab in tabs)
            {
                TabComponent tabComponent = new TabComponent(this);
                tabComponent.SetData(tab);
                tabComponent.UserControlClicked += YourUserControl_UserControlClicked;
                wpTab.Children.Add(tabComponent);
            }
        }
        
        public async Task RefreshAsyncAddProduct()
        {
            wpkassa.Children.Clear();

            Button btn = new Button()
            {
                Name = "btnAddProduct_Click",
                Visibility = Visibility.Visible,
                Height = 120,
                Width = 200,
                Style = (Style)FindResource("ProductCreateButton"),
                Margin = new Thickness(10, 10, 0, 0)
            };

            wpkassa.Children.Add(btn);
            btn.Click += btnAddProduct_Click;
        }
        public void ClearUserControlBorder()
        {

            foreach (var item in wpTab.Children)
            {
                if (item is TabComponent)
                {
                    (item as TabComponent).B.BorderBrush = null;
                }
            }
        }

        private async void btnTabUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (tabId != 0)
            {                
                TabUpdateWindow tabUpdateWindow = new TabUpdateWindow();
                tabUpdateWindow.SetData(tabId);
                tabUpdateWindow.getCashboxId(viewModel.Id);
                tabUpdateWindow.ShowDialog();
                await RefreshAsync();
            }
            else
            {
                MessageBox.Show("Tahrirlash uchun 1 ta Tabni tanlang");
            }
        }

        private async void btnTapDelete_Click(object sender, RoutedEventArgs e)
        {
            if (tabId != 0) 
            {
                if (MessageBox.Show("Rostdan ham bu Tab ni o'chirmoqchimisiz ?", "O'chirish",
                    MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    var dbResult =await _repository.DeleteAsync(tabId);
                    await RefreshAsync();
                }
            }
            else
            {
                MessageBox.Show("O'chirish uchun 1 ta Tabni tanlang");
            }
        }
    }
}
