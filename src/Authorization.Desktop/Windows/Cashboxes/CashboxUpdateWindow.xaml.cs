using Authorization.Desktop.Entities.Cashboxes;
using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Helpers;
using Authorization.Desktop.Interfaces;
using Authorization.Desktop.Repositories.Cashboxes;
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
using System.Windows.Shapes;

namespace Authorization.Desktop.Windows.Cashboxes
{
    /// <summary>
    /// Логика взаимодействия для CashboxUpdateWindow.xaml
    /// </summary>
    public partial class CashboxUpdateWindow : Window
    {
        public long Id { get; set; }

        private CashboxRepository _repository;

        public CashboxUpdateWindow()
        {
            InitializeComponent();
            this._repository = new CashboxRepository();
        }

        private void btnUpdateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void SetData(Cashbox cashbox)
        {
            this.Id = cashbox.Id;
            txtbName.Text = cashbox.Name;
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;

            Cashbox cashbox = new Cashbox();
            if (txtbName.Text.Length >= 3)
            {
                cashbox.Name = txtbName.Text;
                count++;
            }
            else
            {
                MessageBox.Show("Cashbox nomi uzunligi kamida 3 ta bo'lishi kerak");
                return;
            }

            var isShop = await _repository.GetByIdCashboxName(cashbox.Name);
            if (isShop)
            {
                MessageBox.Show("Bunday Cashbox allaqachon bor.");
                return;
            }
            else count++;

            cashbox.Updated_at = TimeHelper.GetDateTime();

            if (count == 2)
            {
                var result = await _repository.UpdateAsync(Id, cashbox);

                if (result > 0)
                {
                    MessageBox.Show("Muvaffaqiyatli tahrirlandi");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Xatolik");
                }
            }
        }
    }
}
