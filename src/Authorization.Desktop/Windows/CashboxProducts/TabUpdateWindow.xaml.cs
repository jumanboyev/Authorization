using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Entities.Subcategories;
using Authorization.Desktop.Entities.Tabs;
using Authorization.Desktop.Helpers;
using Authorization.Desktop.Repositories.Tabs;
using Authorization.Desktop.ViewModels.Tabs;
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
    /// Логика взаимодействия для TabUpdateWindow.xaml
    /// </summary>
    public partial class TabUpdateWindow : Window
    {
        public long tabId {  get; set; }
        public long cashboxId {  get; set; }
        private TabRepository _repository;

        public TabUpdateWindow()
        {
            InitializeComponent();
            this._repository = new TabRepository();
        }

        public void SetData(long Tabid)
        {
            this.tabId = Tabid;
        }

        private void btnUpdateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
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
                MessageBox.Show("Tab uzunligi kamida 3 ta bo'lishi kerak");
                return;
            }

            var isCategory = await _repository.GetByIdTabName(cashboxId, tab.Name);
            if (isCategory)
            {
                MessageBox.Show("Bunday Tab allaqachon bor !");
                return;
            }
            else count++;

            tab.Updated_at = TimeHelper.GetDateTime();

            if (count == 2)
            {
                var result = await _repository.UpdateAsync(tabId, tab);
                if (result > 0)
                {
                    MessageBox.Show("Muvaffaqiyatli tahrirlandi");
                    this.Close();
                }
                else MessageBox.Show("Xatoli");

            }
        }
        public void getCashboxId(long cashboxId)
        {
            this.cashboxId = cashboxId;
        }
    }
}
