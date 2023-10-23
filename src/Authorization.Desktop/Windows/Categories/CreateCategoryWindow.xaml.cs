using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Helpers;
using Authorization.Desktop.Repositories.Categories;
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

namespace Authorization.Desktop.Windows.Categories
{
    /// <summary>
    /// Interaction logic for CreateCategoryWindow.xaml
    /// </summary>
    public partial class CreateCategoryWindow : Window
    {
        public long shopId { get; set; }
        private CategoryRepository _repository;

        public CreateCategoryWindow()
        {
            InitializeComponent();
            this._repository = new CategoryRepository();
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
                Category category = new Category();
                if (txtbName.Text.Length >= 3)
                {
                    category.Name = txtbName.Text;
                    count++;
                }
                else
                {
                    MessageBox.Show("Category nomi uzunligi kamida 3 ta bo'lishi kerak");
                    return;
                }
                category.ShopId = this.shopId;
                category.Created_at = TimeHelper.GetDateTime();
                category.Updated_at = TimeHelper.GetDateTime();

                if (count == 1)
                {
                    var result = await _repository.CreateAsync(category);
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
        public void getShopId(long id)
        {
            this.shopId = id;
        }
    }
}
