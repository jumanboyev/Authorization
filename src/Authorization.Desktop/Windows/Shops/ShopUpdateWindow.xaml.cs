    using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Helpers;
using Authorization.Desktop.Repositories.Shops;
using Authorization.Desktop.ViewModels.Shops;
using Microsoft.Win32;
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

namespace Authorization.Desktop.Windows.Shops
{
    /// <summary>
    /// Interaction logic for ShopUpdateWindow.xaml
    /// </summary>
    public partial class ShopUpdateWindow : Window
    {
        private ShopRepository _repository;
        public long Id {get;set;}

        public ShopUpdateWindow()
        {
            this._repository = new ShopRepository();
            InitializeComponent();
        }

        public void SetData(Shop shop)
        {
            this.Id = shop.Id;
            txtbName.Text = shop.Name;
        }
        private void btnUpdateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;

            Shop shop = new Shop();
            if (txtbName.Text.Length >= 3)
            {
                shop.Name = txtbName.Text;
                count++;
            }
            else
            {
                MessageBox.Show("Do'kon nomi uzunligi kamida 3 ta bo'lishi kerak");
                return;
            }
            shop.Created_at = TimeHelper.GetDateTime();
            shop.Updated_at = TimeHelper.GetDateTime();

            if (count == 1)
            {
                var result = await _repository.UpdateAsync(Id,shop);
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

