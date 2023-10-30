using Authorization.Desktop.Components;
using Authorization.Desktop.Entities.Cashboxes;
using Authorization.Desktop.Interfaces;
using Authorization.Desktop.Repositories.Cashboxes;
using Authorization.Desktop.Windows.Cashboxes;
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

namespace Authorization.Desktop.Pages.Cashboxes
{
    /// <summary>
    /// Логика взаимодействия для CashboxPage.xaml
    /// </summary>
    public partial class CashboxPage : Page
    {
        private CashboxRepository _repository;

        public CashboxPage()
        {
            InitializeComponent();
            this._repository = new CashboxRepository();
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CashboxCreateWindow window = new CashboxCreateWindow();
            window.ShowDialog();
            await RefreshAsync();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();   
        }
        public async Task RefreshAsync()
        {
            MainWP.Children.Clear();
            Button btn = new Button()
            {
                Name = "btnCreate",
                Visibility = Visibility.Visible,
                Height = 130,
                Width = 210,
                Style = (Style)FindResource("ProductCreateButton"),
                Margin = new Thickness(10, 10, 0, 0)

            };
            MainWP.Children.Add(btn);
            btn.Click += btnCreate_Click;

            var cashboxs = await _repository.GetAllAsync();
            foreach (var cashbox in cashboxs)
            {
                CashboxComponent component = new CashboxComponent();
                component.SetData(cashbox);
                MainWP.Children.Add(component);
                component.Refresh = RefreshAsync;
            }
        }
    }
}
