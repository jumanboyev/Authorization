using Authorization.Desktop.Entities.Cashboxes;
using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Repositories.Cashboxes;
using Authorization.Desktop.ViewModels.Cashboxes;
using Authorization.Desktop.Windows.Cashboxes;
using Authorization.Desktop.Windows.CashboxProducts;
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
    /// Логика взаимодействия для CashboxComponent.xaml
    /// </summary>
    public partial class CashboxComponent : UserControl
    {
        public long Id {  get; set; }
        public Func<Task> Refresh {  get; set; }
        private CashboxRepository _repository;
        private CashboxViewModel viewModel;

        public CashboxComponent()
        {
            InitializeComponent();
            this._repository = new CashboxRepository();
            this.viewModel = new CashboxViewModel();
        }
        public void SetData(CashboxViewModel cashboxVoewModel)
        {
            this.viewModel = cashboxVoewModel;
            lbName.Text = cashboxVoewModel.Name;
        }
        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Cashbox cashbox = new Cashbox();
            cashbox.Id = viewModel.Id;
            cashbox.Name = viewModel.Name;

            CashboxUpdateWindow cashboxUpdateWindow = new CashboxUpdateWindow();
            cashboxUpdateWindow.SetData(cashbox);
            cashboxUpdateWindow.ShowDialog();
            await Refresh();
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Rostdan ham bu Cashboxni o'chirmoqchimisiz ?", "O'chirish"
                ,MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                var shopDelete = await _repository.DeleteAsync(viewModel.Id);
                await Refresh();

            }
        }

        private void B_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CashboxProductWindow productWindow = new CashboxProductWindow();
            productWindow.SetData(viewModel);
            productWindow.ShowDialog();
        }
    }
}
