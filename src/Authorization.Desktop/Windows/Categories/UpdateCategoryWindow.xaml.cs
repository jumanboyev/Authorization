using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Helpers;
using Authorization.Desktop.Repositories.Categories;
using Authorization.Desktop.ViewModels.Categories;
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
    /// Interaction logic for UpdateCategoryWindow.xaml
    /// </summary>
    public partial class UpdateCategoryWindow : Window
    {
        public long Id { get; set; }
        private CategoryRepository _repository;

        public UpdateCategoryWindow()
        {
            InitializeComponent();
            this._repository = new CategoryRepository();
        }

        public void SetaData(Category category)
        {
            this.Id = category.Id;
            txtbName.Text = category.Name;
        }
        private void btnUpdateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            Category category =  new Category();

            if(txtbName.Text.Length > 3)
            {
                category.Name = txtbName.Text;
                count++;
            }
            else
            {
                MessageBox.Show("Category uzunligi kamida 3 ta bo'lishi kerak");
                return;
            }

            var isCategory = await _repository.GetByIdCategoryNameAsync(category.Name);
            if (isCategory)
            {
                MessageBox.Show("Bunday Category allaqachon yaratilgan");
                return;
            }
            else count++;

            category.Updated_at = TimeHelper.GetDateTime();
            
            if(count == 2)
            {
                var result = await _repository.UpdateAsync(Id,category);
                if (result > 0)
                {
                    MessageBox.Show("Muvaffaqiyatli tahrirlandi");
                    this.Close();
                }
                else MessageBox.Show("Xatoli");

            }
        }

    }
}
