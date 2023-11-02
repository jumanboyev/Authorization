using Authorization.Desktop.Components;
using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Entities.Products;
using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Entities.Tabs;
using Authorization.Desktop.Repositories.SaleProducts;
using Authorization.Desktop.Repositories.Tabs;
using Authorization.Desktop.Security;
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
        private SaleProductRepository _saleProductRepository;
        private CashboxViewModel viewModel;

        public event PropertyChangedEventHandler? PropertyChanged;
        
        public delegate void OpenMainWindowDelegate();
        public OpenMainWindowDelegate? OpenWindow { get; set; }

        public CashboxProductWindow()
        {
            InitializeComponent();
            this._repository = new TabRepository();
            this._saleProductRepository = new SaleProductRepository();
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
            SaleProductWindow window = new SaleProductWindow();
            window.getTabId(tabId);
            window.ShowDialog();
            await AddProductRefreshAsync();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }

        private async void YourUserControl_UserControlClicked(long id)
        {
            this.tabId = id;
            await AddProductRefreshAsync();

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
        
        public async Task AddProductRefreshAsync()
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

            var saleProducts = await _saleProductRepository.GetAllSaleProductsAsync(tabId);
            foreach ( var saleProduct in saleProducts)
            {
                SaleProductComponent saleProductComponent = new SaleProductComponent();
                saleProductComponent.SetData(saleProduct,viewModel.Id);
                saleProductComponent.RefreshThirdWrapPanel = PaymentRefreshAsync;
                wpkassa.Children.Add(saleProductComponent);

            }
        }

        public void PaymentRefreshAsync()
        {
            wpPayment.Children.Clear();
            var identity = IdentitySingleton.GetInstance();
            var ProductList = identity.AddToCartList;
            float AllTotalSum = 0;

            foreach (var product in ProductList)
            {
                //product.
                if (product.ShopId == this.cashboxId)
                {
                    PaymentComponent paymentComponent = new PaymentComponent();
                    paymentComponent.SetData(product);
                    wpPayment.Children.Add(paymentComponent);
                    AllTotalSum += product.SoldPrice * product.Quantity;
                }
            }
            string FormattedAllTotalPrice = FPrice((AllTotalSum).ToString());
            lbTotalPrice.Content = FormattedAllTotalPrice;
        }
        public string FPrice(string Price)
        {
            float number = float.Parse(Price);
            string formattedNumber = number.ToString("#,##0").Replace(",", " ");
            return formattedNumber;
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
