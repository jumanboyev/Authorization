using Authorization.Desktop.Entities.Cashboxes;
using Authorization.Desktop.Entities.Subcategories;
using Authorization.Desktop.Helpers;
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
    /// Логика взаимодействия для CashboxCreateWindow.xaml
    /// </summary>
    public partial class CashboxCreateWindow : Window
    {

        private CashboxRepository _repository;

        public CashboxCreateWindow()
        {
            InitializeComponent();
            this._repository = new CashboxRepository();
        }

        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txtbName.Text.Length > 0)
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

                var isCategory = await _repository.GetByIdCashboxName(cashbox.Name);
                if (isCategory)
                {
                    MessageBox.Show("Bunday Cashbox allaqachon yaratilgan");
                    return;
                }
                else count++;

                cashbox.Created_at = TimeHelper.GetDateTime();
                cashbox.Updated_at = TimeHelper.GetDateTime();

                if (count == 2)
                {
                    var result = await _repository.CreateAsync(cashbox);
                    if (result > 0) 
                    {
                        MessageBox.Show("Muvaffaqiyatli yaratildi");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Xatolik");
                    }
                }
            }
            else
            {
                MessageBox.Show("Maydon bo'sh bo'lishi mumkin emas!");
            }
        }        
    }
}
