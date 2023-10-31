using Authorization.Desktop.Entities.Cashboxes;
using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Entities.Subcategories;
using Authorization.Desktop.Entities.Tabs;
using Authorization.Desktop.Helpers;
using Authorization.Desktop.Repositories.Tabs;
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

namespace Authorization.Desktop.Windows.CashboxProducts
{
    /// <summary>
    /// Логика взаимодействия для TabCreateWindow.xaml
    /// </summary>
    public partial class TabCreateWindow : Window
    {
        public long cashboxId {  get; set; }
        private TabRepository _repository;

        public TabCreateWindow()
        {
            InitializeComponent();
            this._repository = new TabRepository();
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
                Tab tab = new Tab();

                if (txtbName.Text.Length >= 3)
                {
                    tab.Name = txtbName.Text;
                    count++;
                }
                else
                {
                    MessageBox.Show("Tab nomi uzunligi kamida 3 ta bo'lishi kerak");
                    return;
                }

                var isCashbox = await _repository.GetByIdTabName(cashboxId, tab.Name);
                if (isCashbox)
                {
                    MessageBox.Show("Bunday Tab allaqachon yaratilgan");
                    return;
                }
                else count++;

                tab.CashboxId = this.cashboxId;
                tab.Created_at = TimeHelper.GetDateTime();
                tab.Updated_at = TimeHelper.GetDateTime();

                if (count == 2)
                {
                    var result = await _repository.CreateAsync(tab);
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
        public void getCashboxId(long cashboxId)
        {
            this.cashboxId = cashboxId;
        }
    }
}
