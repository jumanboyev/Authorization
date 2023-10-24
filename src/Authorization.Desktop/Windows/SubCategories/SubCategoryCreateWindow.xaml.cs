using Authorization.Desktop.Entities.Categories;
using Authorization.Desktop.Entities.Subcategories;
using Authorization.Desktop.Helpers;
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
    /// Логика взаимодействия для SubCategoryCreateWindow.xaml
    /// </summary>
    public partial class SubCategoryCreateWindow : Window
    {
        public long CategoryId { get; set; }
        private SubCategoryRepository _repository;

        public SubCategoryCreateWindow()
        {
            InitializeComponent();
            this._repository = new SubCategoryRepository();
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
                SubCategory subCategory = new SubCategory();

                if (txtbName.Text.Length >= 3)
                {
                    subCategory.Name = txtbName.Text;
                    count++;
                }
                else
                {
                    MessageBox.Show("SubCategory nomi uzunligi kamida 3 ta bo'lishi kerak");
                    return;
                }

                var isCategory = await _repository.GetByIdSubCategoryName(subCategory.Name);
                if (isCategory)
                {
                    MessageBox.Show("Bunday SubCategory allaqachon yaratilgan");
                    return;
                }
                else count++;

                subCategory.CategoryId = this.CategoryId;
                subCategory.Created_at = TimeHelper.GetDateTime();
                subCategory.Updated_at = TimeHelper.GetDateTime();

                if (count == 2)
                {
                    var result = await _repository.CreateAsync(subCategory);
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

        public void getCategoryId(long categoryId)
        {
            this.CategoryId = categoryId;
        }
    }
}
