using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Entities.Subcategories;
using Authorization.Desktop.Helpers;
using Authorization.Desktop.Repositories.Categories;
using Authorization.Desktop.Repositories.SubCategories;
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

namespace Authorization.Desktop.Windows.SubCategories
{
    /// <summary>
    /// Логика взаимодействия для SubCategoryUpdateWindow.xaml
    /// </summary>
    public partial class SubCategoryUpdateWindow : Window
    {
        private SubCategoryRepository _repository;

        public long Id { get; set; }

        public SubCategoryUpdateWindow()
        {
            InitializeComponent();
            this._repository = new SubCategoryRepository();

        }

        public void SetaData(SubCategory subCategory)
        {
            this.Id = subCategory.Id;
            txtbName.Text = subCategory.Name;
        }
        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            SubCategory subCategory = new SubCategory();

            if (txtbName.Text.Length > 3)
            {
                subCategory.Name = txtbName.Text;
                count++;
            }
            else
            {
                MessageBox.Show("Category uzunligi kamida 3 ta bo'lishi kerak");
                return;
            }

            var isCategory = await _repository.GetByIdSubCategoryName(subCategory.Name);
            if (isCategory)
            {
                MessageBox.Show("Bunday Category allaqachon yaratilgan");
                return;
            }
            else count++;

            subCategory.Updated_at = TimeHelper.GetDateTime();

            if (count == 2)
            {
                var result = await _repository.UpdateAsync(Id, subCategory);
                if (result > 0)
                {
                    MessageBox.Show("Muvaffaqiyatli tahrirlandi");
                    this.Close();
                }
                else MessageBox.Show("Xatoli");

            }
        }

        private void btnUpdateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
